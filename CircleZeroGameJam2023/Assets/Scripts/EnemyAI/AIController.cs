using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private StateMachineBase stateMachine;
    private TaskSystem taskSystem;
    void Start()
    {
        stateMachine = GetComponent<StateMachineBase>();
        taskSystem = GetComponent<TaskSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isTaskSystemEmpty = taskSystem.IsEmpty();

        taskSystem.SetActive(isTaskSystemEmpty == false);
        stateMachine.SetActive(isTaskSystemEmpty);
    }
}
