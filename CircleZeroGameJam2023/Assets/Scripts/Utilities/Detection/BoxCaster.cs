using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OTBG.Utilities.Detection
{
    public class BoxCaster : CasterBase
    {
        [SerializeField] private Vector2 _boxSize;

        protected override void CastUpdate()
        {
            Collider2D collision = GetOverlappingCollider();
            if (collision == null) return;
            OnTrigger?.Invoke(collision.gameObject);
        }

        public Collider2D GetOverlappingCollider()
        {
            return Physics2D.OverlapBox(transform.position, _boxSize, transform.eulerAngles.z,_layer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector2.zero, _boxSize);
        }
    }
}
