using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

namespace OTBG.Utilities.Extensions
{
    public static class ListExtensions
    {
        public static System.Random random = new System.Random();

        [BurstCompile]
        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToArray().Random();
        }
        [BurstCompile]
        public static T Random<T>(this T[] array)
        {
            return array[random.Next(0, array.Length)];
        }

        public static List<T> GetImmediateComponentsInChildren<T>(this Transform parent) where T : Component
        {
            List<T> immediateChildrenComponents = new List<T>();
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                T component = child.GetComponent<T>();
                if (component != null)
                {
                    immediateChildrenComponents.Add(component);
                }
            }
            return immediateChildrenComponents;
        }

        public static List<T> SelectRange<T>(this List<T> input, int startIndex, int endIndex)
        {
            startIndex = Mathf.Clamp(startIndex, 0, input.Count - 1);
            endIndex = Mathf.Clamp(endIndex, 0, input.Count - 1);

            int count = Mathf.Max(endIndex - startIndex + 1, 0);
            return input.GetRange(startIndex, count);
        }
        public static List<T> SelectRange<T>(this List<T> input, Vector2Int range)
        {
            return SelectRange(input, range.x, range.y);
        }
    }
}