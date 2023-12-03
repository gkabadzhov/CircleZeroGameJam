using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Gameplay.Player.Movement;
using OTBG.Utilities.Data;
using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static event Action<ValueChange> OnDashCooldownChanged;

    private PlayerMovement _playerMovement;
    private Camera _cam;
    private Rigidbody2D _rb;
    private IInputDetector _inputDetector;

    private Coroutine _dashCoroutine;

    [FoldoutGroup("Dash Power"), SerializeField]
    private float minimumForce;
    [FoldoutGroup("Dash Power"), SerializeField]
    private float _dashPowerMultiplier;
    [FoldoutGroup("Dash Power"), SerializeField]
    private float _dashTimer;
    [FoldoutGroup("Cooldown"), SerializeField]
    private float _dashCooldownTimer;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();
        _rb = GetComponent<Rigidbody2D>();

        _inputDetector.OnRightMouseClick += OnRightMouseButtonPressed;
    }

    private void OnDestroy()
    {
        _inputDetector.OnRightMouseClick -= OnRightMouseButtonPressed;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void OnRightMouseButtonPressed()
    {
        if (_dashCoroutine != null)
            return;

        float dashForce = GetDashForce();

        Vector3 playerViewport = _cam.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 direction = (mousePosition - playerViewport).normalized;

        Dash(direction, dashForce);
    }

    public void Dash(Vector3 direction, float dashForce)
    {
        _rb.velocity = Vector3.zero;
        _playerMovement.TriggerForceThrow(direction, dashForce, _dashTimer);

        _dashCoroutine = StartCoroutine(DashCooldown());
    }

    public float GetDashForce()
    {
        float currentMagnitude = Mathf.Max(_rb.velocity.magnitude, minimumForce);
        return currentMagnitude *= _dashPowerMultiplier;   
    }

    public IEnumerator DashCooldown()
    {
        float t = _dashCooldownTimer;
        while(t > 0)
        {
            t -= Time.deltaTime;
            OnDashCooldownChanged?.Invoke(new ValueChange(t,_dashCooldownTimer));
            yield return null;
        }
        _dashCoroutine = null;
    }
}
