using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Core.Three
{
    //在正交摄像机下移动Star到目标地点
    public class ItemAnimation : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _targetRectTransform;

        [SerializeField]
        private RectTransform _parentRectTransform;
        
        [SerializeField]
        private GameObject _itemPrefab;

        [SerializeField]
        private float _startInterval = 0.1f;
        
        [SerializeField] 
        private float _moveDuration = 1.0f;

        [SerializeField] 
        private float _fadeDuration = 0.5f;

        [SerializeField]
        private Ease _moveEase = Ease.InCirc;
        
        [SerializeField]
        private Ease _fadeEase = Ease.InCirc;
        
        private ObjectPool<GameObject> _itemPool;
        
        private readonly Vector2 _farawayPosition = new Vector2(10000, 10000);
        
        private Color _originalColor;

        private Color _targetColor;

        public void DoAnimation(List<GameObject> items)
        {
            if (_items == null || items.Count <= 0) return;

            var targetPosition = _parentRectTransform.InverseTransformPoint(_targetRectTransform.position);
            var starObjects = new GameObject[items.Count];
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var star = _itemPool.Get();
                starObjects[i] = star;

                // reset
                var rectTransform = star.GetComponent<RectTransform>();
                var image = star.GetComponent<Image>();
                image.color = _originalColor;
                var position = item.transform.position;
                rectTransform.localPosition = _parentRectTransform.InverseTransformPoint(position);

                // animation
                var sequence = DOTween.Sequence();
                sequence.PrependInterval(_startInterval * i);
                sequence.Append(rectTransform.DOLocalMove(targetPosition, _moveDuration).SetEase(_moveEase));
                var t = DOTween.To(() => image.color, x => image.color = x, _targetColor, _fadeDuration)
                    .SetEase(_fadeEase);
                t.SetTarget(image);
                sequence.Append(t);
                sequence.onComplete += () => { _itemPool.Release(star); };
            }
        }

        private void Awake()
        {
            if (_parentRectTransform == null || _targetRectTransform == null || _itemPrefab == null)
            {
                this.enabled = false;
            }
            
            _itemPool = new ObjectPool<GameObject>(CreateItem, null, ReleaseItem, DestroyItem);
            _originalColor = _itemPrefab.GetComponent<Image>().color;
            _targetColor = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0);
        }

        private void ReleaseItem(GameObject obj)
        {
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = _farawayPosition;
        }

        private GameObject CreateItem()
        {
            var go = Instantiate(_itemPrefab, _parentRectTransform);
            return go;
        }

        private void DestroyItem(GameObject obj)
        {
            Destroy(obj);
        }
        
#if UNITY_EDITOR
        [SerializeField]
        public List<GameObject> _items = new List<GameObject>();
        
        [ContextMenu("Test")]
        private void Test()
        {
            DoAnimation(_items);
        }
#endif
    }
}