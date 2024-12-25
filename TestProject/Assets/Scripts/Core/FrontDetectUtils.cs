using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretLisa
{
    public class Entity
    {
        public Entity(Vector3 position, Vector3 forward)
        {
            this.position = position;
            this.forward = forward;
        }

        public Vector3 position { get; }

        public Vector3 forward { get; }
    }

    public static class FrontDetectUtils
    {
        public static bool IsInFront(Entity observer, Entity target)
        {
            if (observer.position.Equals(target.position) == true)
            {
                return true;
            }

            var forward = observer.forward;
            var direction = (target.position - observer.position).normalized;
            var dot = Vector3.Dot(direction, forward);
            return dot > 0;
        }
    }

}