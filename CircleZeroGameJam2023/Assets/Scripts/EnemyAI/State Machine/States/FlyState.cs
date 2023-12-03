using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : StateBase
{
    [SerializeField]
    private float flySpeed = 1.5f;
    [SerializeField]
    private float flyTime = 1.5f;
    [SerializeField]
    private AIMoveTask moveTask;

    private Vector2 direction = Vector2.zero;

    public override void OnUpdateState()
    {
        direction = vision.PointOfInterest.transform.position - transform.position;

        AddMoveTask();
    }

    private void AddMoveTask()
    {
        if (moveTask == null) return;

        AIMoveTaskDescription description = new AIMoveTaskDescription();
        description.taskedObject = transform.parent.parent.gameObject;
        description.forwardPosition = direction;
        description.speed = flySpeed;
        description.moveTime = flyTime;

        moveTask.SetDescription(description);

        taskSystem.PushTask(moveTask);
    }
}
