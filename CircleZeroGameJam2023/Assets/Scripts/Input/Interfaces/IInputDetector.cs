using System;
using UnityEngine;

namespace OTBG.Gameplay.Inputs.Interfaces
{
    public interface IInputDetector
    {
        event Action<float> OnHorizontalStateChanged;
        event Action OnInputJump;
        event Action OnReleaseJump;
        event Action OnLeftMouseClick;
        event Action OnRightMouseClick;
        event Action<Vector2> OnDragStart;
        event Action<Vector2> OnDrag;
        event Action<Vector2> OnDragEnd;
        event Action<Vector2> OnMousePositionUpdated;

        void ToggleActive(bool active);
    }
}