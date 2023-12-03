using Dreamteck.Splines;
using OTBG.Gameplay.Player;
using OTBG.Utilities.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomToRoomMover : MonoBehaviour
{
    public UnityEvent OnRoomMovedTo;

    public SplineComputer _spline;

    public SubscribablePhysics2D _pointA;

    public float timeToTravel;
    public float minimumYeetSpeed;
    public Transform _pointB;
    public Transform _sparksVisual;

    private Vector3 lastVel;

    [SerializeField]
    SoundManager _soundManager;

    private void Awake()
    {
        _pointA.OnTriggerEnterEvent += _pointA_OnTriggerEnterEvent;
    }

    private void _pointA_OnTriggerEnterEvent(Collider2D obj)
    {
        if (!obj.transform.TryGetComponent(out PlayerController playerController))
            return;

        Vector3 respawnPoint = gameObject.transform.position;

        playerController.SetRespawnLocation(respawnPoint);
        playerController.PortalToggle(true);
        Rigidbody2D playerRB = playerController.GetComponent<Rigidbody2D>();
        lastVel = playerRB.velocity;
        playerRB.velocity = Vector3.zero;
        StartCoroutine(PortalMovement(playerController));
    }

    public IEnumerator PortalMovement(PlayerController playerController)
    {
        float t = 0;
        _sparksVisual.transform.position = _spline.EvaluatePosition(0);
        ToggleSparksVisual(true);
        while(t < timeToTravel)
        {
            float percentage = t / timeToTravel;

            t += Time.deltaTime;

            Vector3 positionAlong = _spline.EvaluatePosition(percentage);
            _sparksVisual.transform.position = positionAlong;
            playerController.transform.position = positionAlong;
            yield return null;
        }
        playerController.transform.position = _pointB.position;
        ToggleSparksVisual(false);
        playerController.PortalToggle(false);
        Vector3 yeetVel = lastVel.normalized * Mathf.Max(lastVel.magnitude, minimumYeetSpeed);
        playerController.GetComponent<Rigidbody2D>().velocity = yeetVel;
        lastVel = Vector3.zero;
        OnRoomMovedTo?.Invoke();
    }

    public void ToggleSparksVisual(bool isActive)
    {
        _sparksVisual.gameObject.SetActive(isActive);
        _soundManager.SwapClips();
    }
}


