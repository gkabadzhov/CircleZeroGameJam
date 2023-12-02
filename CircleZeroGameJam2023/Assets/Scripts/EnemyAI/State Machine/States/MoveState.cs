using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("MoveState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("MoveState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("MoveState - Leave State");
    }
}
