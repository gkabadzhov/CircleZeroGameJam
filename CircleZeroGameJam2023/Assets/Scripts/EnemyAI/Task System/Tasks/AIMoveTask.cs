using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AIMoveTaskDescription
{
    public GameObject taskedObject;
    public float speed;
    public Vector2 forwardPosition;
    public float moveTime;
}

public class AIMoveTask : AITaskBase
{
    private GameObject  currentTaskedObject;
    private Vector2     forwardPosition = Vector2.zero;
    private float       speed;
    private float       moveTime;
    private Rigidbody2D controllableRB2D;

    private bool        finishedTask = false;

    public void SetDescription(AIMoveTaskDescription description)
    {
        currentTaskedObject = description.taskedObject;
        forwardPosition     = description.forwardPosition;
        speed               = description.speed;
        moveTime            = description.moveTime;

        controllableRB2D    = currentTaskedObject.GetComponent<Rigidbody2D>();
    }

    public override void OnStart()
    {
        base.OnStart();

        finishedTask = false;

        StartCoroutine(Timer());
    }

    public override void OnUpdate() 
    {
        controllableRB2D.AddForce(forwardPosition * speed);
    }

    public override bool IsFinished()
    {
        return finishedTask;
    }

    IEnumerator Timer()
    {
        float delayTime = moveTime;
        yield return new WaitForSeconds(delayTime);

        finishedTask = true;
    }
}
