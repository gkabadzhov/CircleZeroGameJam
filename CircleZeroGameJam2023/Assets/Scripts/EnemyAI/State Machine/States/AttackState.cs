using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("AttackState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("AttackState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("AttackState - Leave State");
    }
}
