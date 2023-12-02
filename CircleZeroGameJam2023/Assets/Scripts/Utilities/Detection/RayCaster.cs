using OTBG.Utilities.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OTBG.Utilities.Detection
{
    public class RayCaster : CasterBase
    {
        [SerializeField] private float _distance;
        [SerializeField, Vector3Visualiser] private Vector3 _direction;
        protected override void CastUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, _distance, _layer);
            if (hit.collider == null) return;
            OnTrigger?.Invoke(hit.collider.gameObject);
        }

        public float GetDistance() => _distance;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector2 rayEnd = (_direction * _distance) + transform.position;
            Gizmos.DrawLine(transform.position, rayEnd);
        }
    }
}
