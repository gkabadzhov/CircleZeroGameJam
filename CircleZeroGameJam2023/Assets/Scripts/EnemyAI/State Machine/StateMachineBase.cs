using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
    private StateBase   currentState;
    private bool        currentlyActive = false;

    void Start()
    {
        if (currentState == null) return;

        currentState.OnStartState();
    }

    void Update()
    {
        if(currentState == null || currentlyActive == false) return;

        currentState.OnUpdateState();
        StateBase newState = currentState.OnEvaluateEdges();
        if (newState != currentState)
        {
            currentState.OnLeaveState();
            currentState = newState;
            currentState.OnStartState();
        }
    }

    public void SetActive(bool value)
    {
        currentlyActive = value;
    }
}
