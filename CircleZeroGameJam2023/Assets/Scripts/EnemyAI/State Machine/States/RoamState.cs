using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : StateBase
{
    [SerializeField]
    private float roamSpeed = 1.0f;
    [SerializeField]
    private float roamTime = 1.0f;
    [SerializeField]
    private AIMoveTask moveTask;

    Vector2 roamPosition = Vector2.left;

    public override void OnUpdateState()
    {
        AddMoveTask();

        roamPosition = (roamPosition == Vector2.left) ? Vector2.right : Vector2.left;
    }

    private void AddMoveTask()
    {
        if (moveTask == null) return;

        AIMoveTaskDescription description   = new AIMoveTaskDescription();
        description.taskedObject            = transform.parent.parent.gameObject;
        description.forwardPosition         = roamPosition;
        description.speed                   = roamSpeed;
        description.moveTime                = roamTime;

        moveTask.SetDescription(description);

        taskSystem.PushTask(moveTask);
    }
}
