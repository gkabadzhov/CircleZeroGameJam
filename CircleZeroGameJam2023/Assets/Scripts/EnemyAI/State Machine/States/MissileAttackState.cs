using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackState : StateBase
{
    [SerializeField]
    private Bullet projectilePrefab;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private Transform emitter;
    [SerializeField]
    private AIShootTask shootTask;

    private Vector2 direction = Vector2.zero;

    public override void OnUpdateState()
    {
        direction = (vision.PointOfInterest.transform.position + Vector3.up) - emitter.position;

        direction.Normalize();

        AddMissileTask();
    }

    private void AddMissileTask()
    {
        if (shootTask == null) return;

        AIShootTaskDescription description = new AIShootTaskDescription();
        description.taskedObject = transform.parent.parent.gameObject;
        description.direction = direction;
        description.projectilePrefab = projectilePrefab;
        description.speed = speed;
        description.cooldownTime = cooldownTime;
        description.emitter = emitter;

        shootTask.SetDescription(description);

        taskSystem.PushTask(shootTask);
    }
}
