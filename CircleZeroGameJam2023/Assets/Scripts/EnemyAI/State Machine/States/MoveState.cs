using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateBase
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float moveTime = 1.0f;
    [SerializeField]
    private AIMoveTask moveTask;

    private Vector2 direction = Vector2.zero;

    public override void OnUpdateState()
    {
        Vector2 rawDirection = vision.PointOfInterest.transform.position - transform.position;

        rawDirection.Normalize();

        direction = rawDirection.x > 0 ? Vector2.right : (rawDirection.x < 0 ? Vector2.left : Vector2.zero);

        AddMoveTask();
    }

    private void AddMoveTask()
    {
        if (moveTask == null) return;

        AIMoveTaskDescription description = new AIMoveTaskDescription();
        description.taskedObject = transform.parent.parent.gameObject;
        description.forwardPosition = direction;
        description.speed = moveSpeed;
        description.moveTime = moveTime;

        moveTask.SetDescription(description);

        taskSystem.PushTask(moveTask);
    }
}
