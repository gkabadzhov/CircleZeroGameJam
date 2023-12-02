using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackState : StateBase
{
    public override void OnStartState()
    {
        Debug.Log("MissileAttackState - Start State");
    }

    public override void OnUpdateState()
    {
        Debug.Log("MissileAttackState - Update State");
    }

    public override void OnLeaveState()
    {
        Debug.Log("MissileAttackState - Leave State");
    }
}
