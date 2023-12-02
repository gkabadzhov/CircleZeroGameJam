using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDistanceEdge : EdgeBase
{
    [SerializeField]
    private float meleeDistance = 5.0f;
    public override bool Evaluate()
    {
        EnemyVision vision = GetComponentInParent<EnemyVision>();
        if (vision != null && vision.PointOfInterest != null)
        {
            float currentDistanceToPOI = Vector2.Distance(this.transform.position, vision.PointOfInterest.transform.position);

            return Mathf.Abs(currentDistanceToPOI) <= meleeDistance;
        }

        return false;
    }
}
