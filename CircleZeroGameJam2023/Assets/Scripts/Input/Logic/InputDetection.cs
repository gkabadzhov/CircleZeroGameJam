using OTBG.Applications.Managers;
using OTBG.Gameplay.Inputs.Interfaces;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OTBG.Gameplay.Inputs.Logic
{
    public class InputDetection : MonoBehaviour, IInputDetector, IPausable
    {
        public event Action<float> OnHorizontalStateChanged;
        public event Action OnInputJump;
        public event Action OnReleaseJump;
        public event Action<Vector2> OnDragStart;
        public event Action<Vector2> OnDrag;
        public event Action<Vector2> OnDragEnd;
        public event Action<Vector2> OnMousePositionUpdated;
        public event Action OnLeftMouseClick;
        public event Action OnRightMouseClick;

        [FoldoutGroup("Settings")]
        [SerializeField]
        private bool _isDetecting = true;

        private Vector2 _startDragPosition;
        private bool _isDragging = false;

        // Update is called once per frame
        void Update()
        {
            if (!_isDetecting)
                return;

            DetectInput();
        }

        private void DetectInput()
        {
            float horizontalState = Input.GetAxisRaw("Horizontal");
            OnHorizontalStateChanged?.Invoke(horizontalState);

            OnMousePositionUpdated?.Invoke(Input.mousePosition);

            if (Input.GetButtonDown("Jump"))
            {
                OnInputJump?.Invoke();
            }
            if (Input.GetButtonUp("Jump"))
            {
                OnReleaseJump?.Invoke();
            }

            // Check if the pointer is over a UI element when starting the drag
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    _startDragPosition = Input.mousePosition;
                    _isDragging = true;
                    OnDragStart?.Invoke(_startDragPosition);
                }
            }

            if (_isDragging)
            {
                OnDrag?.Invoke(Input.mousePosition);
            }

            // Check if the pointer is over a UI element when ending the drag
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    OnDragEnd?.Invoke(Input.mousePosition);
                }
            }
        }

        public void Pause()
        {
            ToggleActive(false);
        }

        public void UnPause()
        {
            ToggleActive(true);
        }

        public void ToggleActive(bool active)
        {
            _isDetecting = active;
        }
    }
}
