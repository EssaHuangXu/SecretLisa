using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SecretLisa.Editor
{
    public class JsonSearchEditor : EditorWindow
    {
        private string _searchPath = string.Empty;
        
        private List<string> _results = new List<string>();
        
        [MenuItem("Utils/Json Search")]
        public static void CreateWindow()
        {
            var window = GetWindow(typeof(JsonSearchEditor));
            window.Show();
        }
        
        public void OnGUI()
        {
            if (GUILayout.Button("Select Folder"))
            {
                SelectFolder();
            }

            EditorGUILayout.LabelField("Search Path:" + _searchPath);

            if (GUILayout.Button("Search") && string.IsNullOrWhiteSpace(_searchPath) == false)
            {
                Search();
            }

            if (_results.Count > 0)
            {
                ShowResults();
            }
        }

        private void SelectFolder()
        {
            var folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (folderPath.Length != 0)
            {
                _searchPath = folderPath;
            }
            _results.Clear();
        }
        
        private void Search()
        {
            _results.Clear();
            var paths = Directory.GetFiles(_searchPath,  "*.json");

            foreach (var t in paths)
            {
                try
                {
                    var model = JsonUtility.FromJson<JsonModel>(File.ReadAllText(t));
                    if (model != null && model.num1 == model.num2)
                    {
                        _results.Add(t);
                    }
                }
                catch (Exception _)
                {
                    // ignored
                }
            }
        }

        private void ShowResults()
        {
            EditorGUILayout.LabelField("Results:");
            for (var i = 0; i < _results.Count; i++)
            {
                // this is a bug when use EditorGuiLayout
                // if (!EditorGUILayout.LinkButton(path)) continue;
                //     
                // var instanceID = AssetDatabase.LoadAssetAtPath(path.Replace(Application.dataPath, "Assets"), typeof(TextAsset)).GetInstanceID();
                // EditorGUIUtility.PingObject(instanceID);
                var path = _results[i];
                var size = EditorStyles.linkLabel.CalcSize(new GUIContent(path));
                if (!EditorGUI.LinkButton(new Rect(10, 80 + EditorGUIUtility.singleLineHeight * i, size.x,
                        EditorGUIUtility.singleLineHeight), path)) continue;
                var instanceID = AssetDatabase
                    .LoadAssetAtPath(path.Replace(Application.dataPath, "Assets"), typeof(TextAsset)).GetInstanceID();
                EditorGUIUtility.PingObject(instanceID);
            }
        }
    }
}