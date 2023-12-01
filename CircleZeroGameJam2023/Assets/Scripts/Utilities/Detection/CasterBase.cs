using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace OTBG.Utilities.Detection
{
    public class CasterBase : MonoBehaviour
    {
        public UnityEvent<GameObject> OnTrigger;

        [SerializeField] protected LayerMask _layer;
        [SerializeField] private bool _startOnAwake = true;

        private void Awake()
        {
            if (_startOnAwake) StartCasting();
        }

        public virtual void StartCasting()
        {
            StopCasting();
            _castingCoroutine = StartCoroutine(CastingCoroutine());
        }

        private Coroutine _castingCoroutine;
        protected virtual IEnumerator CastingCoroutine()
        {
            while (true)
            {
                CastUpdate();
                yield return null;
            }
        }

        protected virtual void CastUpdate()
        {

        }

        public virtual void StopCasting()
        {
            if (_castingCoroutine != null) StopCoroutine(_castingCoroutine);
        }
    }
}