using OTBG.PlayFab.Management;
using UnityEngine;

namespace OTBG.Utilities.General
{
    /// <summary>
    /// Singleton
    /// Parent script to be derived from
    /// Allows Manager Scripts to work between scenes
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindFirstObjectByType<T>();
				}
				return instance;
			}
			set
			{
				instance = value;
			}
		}
		protected virtual void Awake()
		{
			if (Instance == null)
				Instance = FindFirstObjectByType<T>();
			else
				if (Instance != this)
				Destroy(gameObject);
			DontDestroyOnLoad(gameObject);
		}
	}
}