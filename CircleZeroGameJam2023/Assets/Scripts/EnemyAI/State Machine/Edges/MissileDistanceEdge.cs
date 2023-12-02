using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDistanceEdge : EdgeBase
{
    [SerializeField]
    private float missileDistance = 20.0f;
    public override bool Evaluate()
    {
        EnemyVision vision = GetComponentInParent<EnemyVision>();
        if (vision != null && vision.PointOfInterest != null)
        {
            float currentDistanceToPOI = Vector2.Distance(this.transform.position, vision.PointOfInterest.transform.position);

            return Mathf.Abs(currentDistanceToPOI) <= missileDistance;
        }

        return false;
    }
}
