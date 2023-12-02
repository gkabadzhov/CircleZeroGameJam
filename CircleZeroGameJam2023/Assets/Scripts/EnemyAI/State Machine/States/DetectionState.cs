using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("DetectionState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("DetectionState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("DetectionState - Leave State");
    }
}
