using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITaskBase : MonoBehaviour
{
    private bool taskStarted = false;
    public virtual void OnStart() 
    {
        taskStarted = true;
    }
    public virtual void OnUpdate() { }
    public virtual void OnLeave()
    {
        taskStarted = false;
    }
    
    public bool IsTaskStarted()
    {
        return taskStarted;
    }
    public virtual bool IsFinished()
    {
        return true; 
    }
}
