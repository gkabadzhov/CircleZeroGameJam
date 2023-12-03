using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AIShootTaskDescription
{
    public GameObject   taskedObject;
    public Vector2      direction;
    public Bullet       projectilePrefab;
    public float        speed;
    public float        cooldownTime;
    public Transform    emitter;
}

public class AIShootTask : AITaskBase
{
    private GameObject  taskedObject;
    private Vector2     direction;
    private Bullet      projectilePrefab;
    private float       speed;
    private float       cooldownTime;
    private Transform   emitter;

    private bool isFinished = false;
    public void SetDescription(AIShootTaskDescription description)
    {
        taskedObject = description.taskedObject;
        direction = description.direction;
        projectilePrefab = description.projectilePrefab;
        speed = description.speed;
        cooldownTime = description.cooldownTime;
        emitter = description.emitter;
    }

    public override void OnStart()
    {
        base.OnStart();

        isFinished = false;

        CreateBullet();

        StartCoroutine(ShotDelay(cooldownTime));
    }
    public override bool IsFinished()
    {
        return isFinished;
    }
    private void CreateBullet()
    {
        Bullet bulletInstance = Instantiate(projectilePrefab, emitter.position, emitter.rotation);

        direction.Normalize();

        bulletInstance.BulletInit(direction, speed);
    }

    public IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFinished = true;
    }
}
