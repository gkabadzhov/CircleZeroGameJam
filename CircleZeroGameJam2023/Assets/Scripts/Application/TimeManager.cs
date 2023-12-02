using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OTBG.Applications.Managers
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        public static event Action<bool> OnPauseStateChanged;

        [FoldoutGroup("Time Settings")]
        [SerializeField] private float _defaultTimeScale = 1f;

        [FoldoutGroup("Time Settings")]
        [SerializeField] private float _defaultFixedDeltaTime = 0.02f;

        private Stack<float> _timeScaleStack = new Stack<float>();
        private IEnumerator _timeScaleCoroutine;

        private void Start()
        {
            ResetTimeScale();
        }

        [Button]
        public void SetTimeScale(float newTimeScale, float transitionTime)
        {
            if (_timeScaleCoroutine != null)
            {
                StopCoroutine(_timeScaleCoroutine);
                _timeScaleCoroutine = null;
            }

            _timeScaleCoroutine = ChangeTimeScaleOverTime(newTimeScale, transitionTime);
            StartCoroutine(_timeScaleCoroutine);
        }

        public void PauseGame()
        {
            _timeScaleStack.Push(Time.timeScale);
            OnPauseStateChanged?.Invoke(true);
            SetTimeScale(0f, 0f);
            
        }

        public void UnpauseGame()
        {
            if (_timeScaleStack.Count > 0)
            {
                float previousTimeScale = _timeScaleStack.Pop();
                SetTimeScale(previousTimeScale, 0f);
            }
            OnPauseStateChanged?.Invoke(false);
        }

        public void ResetTimeScale()
        {
            Time.timeScale = _defaultTimeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
        }

        private IEnumerator ChangeTimeScaleOverTime(float newTimeScale, float transitionTime)
        {
            float startTime = Time.realtimeSinceStartup;
            float endTime = startTime + transitionTime;
            float initialTimeScale = Time.timeScale;

            while (Time.realtimeSinceStartup < endTime)
            {
                float t = (Time.realtimeSinceStartup - startTime) / transitionTime;
                Time.timeScale = Mathf.Lerp(initialTimeScale, newTimeScale, t);
                Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
                yield return null;
            }

            Time.timeScale = newTimeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;
            _timeScaleCoroutine = null;
        }
    }
    public interface IPausable
    {
        public void Pause();
        public void UnPause();
    }
}