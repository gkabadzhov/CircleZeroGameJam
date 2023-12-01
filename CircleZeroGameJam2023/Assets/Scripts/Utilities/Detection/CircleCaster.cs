using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OTBG.Utilities.Detection
{
    public class CircleCaster : CasterBase
    {
        [FoldoutGroup("Stats"),SerializeField] private float _radius;

        protected override void CastUpdate()
        {
            Collider2D collider = GetCollider();
            if (collider == null) return;
            OnTrigger?.Invoke(collider.gameObject);
        }

        private Collider2D GetCollider()
        {
            return Physics2D.OverlapCircle(transform.position, _radius,_layer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public void SetRadius(float radius)
        {
            _radius = radius;
        }

        public float GetRadius()
        {
            return _radius;
        }
    }
}
