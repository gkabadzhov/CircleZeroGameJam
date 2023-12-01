using System;
using UnityEngine;

namespace OTBG.Utilities.General
{
    public static class Vector2Utils
    {
        public static Vector2 GetXDirectionTo(Transform start, Transform end)
        {
            return GetXDirectionTo(start.position, end.position);
        }

        public static Vector2 GetXDirectionTo(Vector2 start, Vector2 end)
        {
            return end.x - start.x > 0 ? Vector2.right : Vector2.left;
        }
        public static Vector2 GetDirectionTo(Transform start, Transform end)
        {
            return GetDirectionTo(start.position, end.position);
        }

        public static Vector2 GetDirectionTo(Vector2 start, Vector2 end)
        {
            return (end - start).normalized;
        }
        public static Vector2 GetPointBetween(Vector2 start, Vector2 end, float percentage)
        {
            return Vector2.Lerp(start, end, percentage);
        }

        public static void GetPointPosition(Vector2 origin, Vector2 direction, float length, LayerMask targetLayers, Action<Vector3> OnPosition = null)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, length, targetLayers);
            if (hit.transform == null)
                return;

            OnPosition?.Invoke(hit.point);
        }
    }

}