using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGroundedState : StateBase
{
    [SerializeField]
    private float minimumForce = 15.0f;
    [SerializeField]
    private float dashPowerMultiplier = 2.0f;
    [SerializeField]
    private float dashTimer = 1.5f;

    private Vector2 direction = Vector2.zero;

    public override void OnUpdateState()
    {
        Vector2 rawDirection = vision.PointOfInterest.transform.position - transform.position;

        rawDirection.Normalize();

        direction = rawDirection.x > 0 ? Vector2.right : (rawDirection.x < 0 ? Vector2.left : Vector2.zero);

        AddDashTask();
    }

    private void AddDashTask()
    {
        AIDashTask dashTask = FindObjectOfType<AIDashTask>();

        AIDashTaskDescription description = new AIDashTaskDescription();
        description.taskedObject = transform.parent.parent.gameObject;
        description.direction = direction;
        description.minimumForce = minimumForce;
        description.dashPowerMultiplier = dashPowerMultiplier;
        description.dashTimer = dashTimer;

        dashTask.SetDescription(description);

        taskSystem.PushTask(dashTask);
    }
}
