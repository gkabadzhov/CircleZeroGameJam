using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Utilities.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldObjectToMouse : MonoBehaviour
{
    public event Action<bool> OnAimDirectionChanged;

    private IInputDetector _inputDetector;
    private Camera _cam;

    [SerializeField]
    private Vector2 _targetDirection;

    // Threshold for horizontal direction change
    [SerializeField]
    private float HorizontalChangeThreshold = 0.05f;
    public bool _lastAimingRight = true;

    private void Awake()
    {
        _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();
        _inputDetector.OnMousePositionUpdated += _inputDetector_OnMousePositionUpdated;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void OnDestroy()
    {
        _inputDetector.OnMousePositionUpdated -= _inputDetector_OnMousePositionUpdated;
    }

    private void Update()
    {
        Vector2 direction = GetMouseDirection();
        _targetDirection = direction;

        // Check if horizontal direction change is significant
        bool isAimingRight = _targetDirection.x > 0;
        if (Mathf.Abs(_targetDirection.x) > HorizontalChangeThreshold && isAimingRight != _lastAimingRight)
        {
            OnAimDirectionChanged?.Invoke(isAimingRight);
            _lastAimingRight = isAimingRight;
        }

        RotateTowards(direction);
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // Reset Z axis for 2D
        return (mouseWorldPosition - transform.position).normalized;
    }

    private void RotateTowards(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }
    }

    private void _inputDetector_OnMousePositionUpdated(Vector2 obj)
    {
        Vector3 worldPosition = _cam.ScreenToWorldPoint(new Vector3(obj.x, obj.y, _cam.nearClipPlane));
        Vector3 direction = (worldPosition - transform.position).normalized;
        RotateTowards(direction);
    }

    private void OnDrawGizmos()
    {
        if (_cam != null)
        {
            Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, mouseWorldPosition);
            Gizmos.DrawSphere(mouseWorldPosition, 0.1f);
        }
    }
}
