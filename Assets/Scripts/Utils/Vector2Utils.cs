using UnityEngine;

namespace Utils
{
    public static class Vector2Utils
    {
        public static Vector2 Clamp(this Vector2 vector, float min, float max)
        {
            var x = Mathf.Clamp(vector.x, min, max);
            var y = Mathf.Clamp(vector.y, min, max);

            return new Vector2(x, y);
        }

        public static Vector2 AddY(this Vector3 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }
    }
}