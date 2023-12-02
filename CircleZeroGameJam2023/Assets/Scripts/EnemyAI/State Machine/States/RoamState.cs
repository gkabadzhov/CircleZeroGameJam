using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("RoamState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("RoamState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("RoamState - Leave State");
    }
}
