using System.Collections.Generic;
using UnityEngine;

namespace OTBG.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static T[] FindObjectsOfInterface<T>(this GameObject[] objects) where T : class
        {
            List<T> interfaceObjects = new List<T>();
            foreach (GameObject obj in objects)
            {
                if (obj is T)
                    interfaceObjects.Add(obj as T);
            }
            return interfaceObjects.ToArray();
        }

        public static T FindInterfaceInScene<T>(this GameObject[] objects) where T : class
        {
            foreach (GameObject obj in objects)
            {
                if (obj.TryGetComponent(out T comp))
                {
                    return comp;
                }
            }
            return null;
        }

        public static T FindInterfaceInScene<T>() where T : class
        {
            return Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None).FindInterfaceInScene<T>();
        }
    }
}
