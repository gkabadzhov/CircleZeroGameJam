using OTBG.Utilities.Extensions;
using UnityEngine;

namespace OTBG.Utilities.General
{
    public static class UtilityFuncs
    {
        public static T[] FindInterfacesInScene<T>(bool findNotActive = false) where T : class
        {
            return UnityEngine.Object.FindObjectsOfType<GameObject>(findNotActive).FindObjectsOfInterface<T>();
        }

        public static T FindInterfaceInScene<T>(bool findNotActive = false) where T : class
        {
            return UnityEngine.Object.FindObjectsOfType<GameObject>(findNotActive).FindInterfaceInScene<T>();
        }
    }

}