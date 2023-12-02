using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecisionState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("AttackDecisionState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("AttackDecisionState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("AttackDecisionState - Leave State");
    }
}
