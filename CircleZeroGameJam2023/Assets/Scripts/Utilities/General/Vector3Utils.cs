using UnityEngine;

namespace OTBG.Utilities.General
{
    public static class Vector3Utils
    {
        public static Vector3 GetXDirectionTo(Transform start, Transform end)
        {
            return GetXDirectionTo(start.position, end.position);
        }

        public static Vector3 GetXDirectionTo(Vector3 start, Vector3 end)
        {
            return end.x - start.x > 0 ? Vector2.right : Vector2.left;
        }

        public static Vector3 GetDirectionTo(Transform start, Transform end)
        {
            return GetDirectionTo(start.position, end.position);
        }

        public static Vector3 GetDirectionTo(Vector3 start, Vector3 end)
        {
            return (end - start).normalized;
        }

        public static Vector3 GetPointBetween(Vector3 start, Vector3 end, float percentage)
        {
            return Vector3.Lerp(start, end, percentage);
        }
        public static Vector3 ConvertVector2To3(Vector2 direction)
        {
            return new Vector3(direction.x, direction.y, 0);
        }

    }
}