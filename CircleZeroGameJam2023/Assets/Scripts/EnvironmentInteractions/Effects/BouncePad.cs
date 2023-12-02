using OTBG.Gameplay.Player.Movement;
using OTBG.Utilities.General;
using System.Collections;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField]
    private float _bounceForce;
    [SerializeField]
    private float _bounceCooldown;
    [SerializeField]
    private float _controlStopTimer;

    private SubscribablePhysics2D _bounceTrigger;
    private Coroutine _bounceDelayCoroutine;

    private void Awake()
    {
        _bounceTrigger = GetComponentInChildren<SubscribablePhysics2D>();

        _bounceTrigger.OnTriggerEnterEvent += _bounceTrigger_OnTriggerEnterEvent;
    }

    private void _bounceTrigger_OnTriggerEnterEvent(Collider2D obj)
    {
        if (_bounceDelayCoroutine != null)
            return;

        if (!obj.TryGetComponent(out PlayerMovement playerMovement))
            return;

        BouncePlayer(playerMovement);
        _bounceDelayCoroutine = StartCoroutine(BounceCooldown()); 
    }


    public IEnumerator BounceCooldown()
    {
        yield return new WaitForSeconds(_bounceCooldown);
        _bounceDelayCoroutine = null;
    }

    public void BouncePlayer(PlayerMovement playerMovement)
    {
        playerMovement.ForceStop();
       playerMovement.TriggerForceThrow(transform.up, _bounceForce, _controlStopTimer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
