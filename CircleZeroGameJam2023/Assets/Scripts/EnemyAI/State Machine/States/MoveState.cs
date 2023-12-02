using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateBase
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    private Vector2 direction = Vector2.zero;

    public override void OnUpdateState()
    {
        direction = vision.PointOfInterest.transform.position - transform.position;

        direction.Normalize();

        AddMoveTask();
    }

    private void AddMoveTask()
    {
        AIMoveTask moveTask = FindObjectOfType<AIMoveTask>();

        AIMoveTaskDescription description = new AIMoveTaskDescription();
        description.taskedObject = transform.parent.parent.gameObject;
        description.forwardPosition = direction;
        description.speed = moveSpeed;
        description.moveTime = 2.0f;

        moveTask.SetDescription(description);

        taskSystem.PushTask(moveTask);
    }
}
