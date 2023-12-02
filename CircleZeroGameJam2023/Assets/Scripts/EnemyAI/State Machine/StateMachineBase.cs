using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
    [SerializeField]
    private StateBase   currentState;

    void Start()
    {
        if (currentState == null) return;

        currentState.OnStartState();
    }

    void Update()
    {
        if(currentState == null) return;

        currentState.OnUpdateState();
        StateBase newState = currentState.OnEvaluateEdges();
        if (newState != currentState)
        {
            currentState.OnLeaveState();
            currentState = newState;
            currentState.OnStartState();
        }
    }


}
