using OTBG.Gameplay.Player.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AIDashTaskDescription
{
    public GameObject taskedObject;
    public Vector2 direction;
    public float minimumForce;
    public float dashPowerMultiplier;
    public float dashTimer;
}

public class AIDashTask : AITaskBase
{
    private GameObject taskedObject;
    private Vector2 direction = Vector2.zero;
    private float minimumForce;
    private float dashPowerMultiplier;
    private float dashTimer;

    private Rigidbody2D controllableRB2D;

    private bool isFinished = false;

    public void SetDescription(AIDashTaskDescription description)
    {
        taskedObject        = description.taskedObject;
        minimumForce        = description.minimumForce;
        dashPowerMultiplier = description.dashPowerMultiplier;
        dashTimer           = description.dashTimer;
        direction           = description.direction;

        controllableRB2D    = taskedObject.GetComponent<Rigidbody2D>();
    }

    public override void OnStart()
    {
        base.OnStart();

        isFinished = false;

        Dash(direction, GetDashForce());
    }

    private void Dash(Vector2 direction, float dashForce)
    {
        controllableRB2D.velocity = Vector3.zero;

        this.GetComponentInParent<HealthController>().SetInvulnerability(true);

        StartCoroutine(ForceThrow(direction, dashForce, dashTimer));
    }
    public override bool IsFinished()
    {
        animator.SetBool("EntityDashing", controllableRB2D.velocity.x <= 0.0f);

        return isFinished;
    }

    private float GetDashForce()
    {
        float currentMagnitude  = Mathf.Max(controllableRB2D.velocity.magnitude, minimumForce);
        return currentMagnitude *= dashPowerMultiplier;
    }
    public IEnumerator ForceThrow(Vector2 direction, float force, float timer = 0)
    {
        controllableRB2D.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(timer);
        this.GetComponentInParent<HealthController>().SetInvulnerability(false);
        isFinished = true;
    }

}
