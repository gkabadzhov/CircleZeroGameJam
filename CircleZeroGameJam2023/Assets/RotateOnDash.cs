using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnDash : MonoBehaviour
{
    public void Awake()
    {
        PlayerDash.OnDashDirection += PlayerDash_OnDashDirection;
        PlayerDash.OnDashStateChanged += PlayerDash_OnDashStateChanged;
    }


    private void OnDestroy()
    {
        PlayerDash.OnDashDirection -= PlayerDash_OnDashDirection;
        PlayerDash.OnDashStateChanged -= PlayerDash_OnDashStateChanged;
    }

    private void PlayerDash_OnDashStateChanged(bool obj)
    {
        if (obj == false)
            transform.rotation = Quaternion.identity;
    }
    private void PlayerDash_OnDashDirection(Vector3 obj)
    {
        if (obj.x <= 0)
        {
            transform.right = -obj.normalized;
            return;
        }
        transform.right = obj.normalized;
    }
}
