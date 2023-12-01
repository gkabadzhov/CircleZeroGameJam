using OTBG.Utilities.General;
using System.Linq;
using UnityEngine;

namespace OTBG.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static void Clear(this Transform transform)
        {
            foreach(Transform child in transform.GetComponentsInChildren<Transform>()
                .Where(t => t.GetComponent<DontDestroyOnClear>() == null))
            {
                if (child == transform)
                    continue;
                Object.Destroy(child.gameObject);
            }
        }

        public static void Clear<T>(this Transform transform) where T : MonoBehaviour
        {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>()
                .Where(t => t.GetComponent<T>() != null && 
                            t.GetComponent<DontDestroyOnClear>() == null))
            {
                if (child == transform)
                    continue;
                Object.Destroy(child.gameObject);
            }
        }
    }
}
