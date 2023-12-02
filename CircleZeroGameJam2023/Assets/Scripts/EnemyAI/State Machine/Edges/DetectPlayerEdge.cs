using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerEdge : EdgeBase
{
    [SerializeField]
    private float radius = 0.0f;
    [SerializeField]
    private LayerMask layer;

    private Collider2D GetCollider()
    {
        return Physics2D.OverlapCircle(transform.position, radius, layer);
    }

    public override bool Evaluate()
    {
        Collider2D collider = GetCollider();
        if (collider == null)
        {
            return false;
        }

        EnemyVision vision = transform.parent.GetComponentInParent<EnemyVision>();
        if (vision != null)
        {
            vision.PointOfInterest = collider.gameObject;
        }

        return true;
    }
}
