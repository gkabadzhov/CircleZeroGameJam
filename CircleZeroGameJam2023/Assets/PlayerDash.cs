using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Utilities.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Camera _cam;
    private IInputDetector _inputDetector;

    private void Awake()
    {
        _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();

        _inputDetector.OnMousePositionUpdated += _inputDetector_OnMousePositionUpdated;
        _inputDetector.OnLeftMouseClick += _inputDetector_OnLeftMouseClick;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void _inputDetector_OnLeftMouseClick()
    {
        Vector3 playerViewport = _cam.WorldToViewportPoint(transform.position);
        //Vector3 mousePosition = Input.mouse
    }

    private void _inputDetector_OnMousePositionUpdated(Vector2 obj)
    {
        
    }
}
