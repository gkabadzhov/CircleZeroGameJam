using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    [SerializeField]
    AITaskBase task;
    [SerializeField]
    private bool                currentlyActive = false;

    // Update is called once per frame
    void Update()
    {
        if (IsEmpty())
        {
            return;
        }    

        if (task.IsTaskStarted() == false)
        {
            task.OnStart();
        }
        else if (task.IsFinished())
        {
            task.OnLeave();
            task = null;
        }
        else
        {
            task.OnUpdate();
        }
    }

    public void PushTask(AITaskBase newTask)
    {
        task = newTask;
    }

    public bool IsEmpty()
    {
        return task == null;
    }
    public void SetActive(bool value)
    {
        currentlyActive = value;
    }
}