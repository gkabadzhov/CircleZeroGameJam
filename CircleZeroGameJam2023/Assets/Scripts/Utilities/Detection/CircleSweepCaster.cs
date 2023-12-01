using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OTBG.Utilities.Detection
{
    public class CircleSweepCaster : CasterBase
    {
        public event Action<RaycastHit2D[]> OnHitsDetected;
        [SerializeField] private float _projectileSize;
        private Vector2 _lastPos;

        private void OnEnable()
        {
            _lastPos = transform.position;
        }

        protected override void CastUpdate()
        {
            Vector2 castDirection = ((Vector2)transform.position - _lastPos).normalized;
            float distance = Vector2.Distance(transform.position, _lastPos);
            var hits = Physics2D.CircleCastAll(_lastPos, _projectileSize, castDirection, distance, _layer);
            if(hits.Length > 0) OnHitsDetected?.Invoke(hits);
            _lastPos = transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _projectileSize);
        }
    }
}
