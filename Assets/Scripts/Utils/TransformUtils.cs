using UnityEngine;

namespace Utils
{
    public static class TransformUtils
    {
        public static void SetPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }

        public static void AddYPos(this Transform target, float y)
        {
            target.position = target.position.AddY(y);
        }
    }
}