using System;
using UnityEngine;

namespace OTBG.Gameplay.Inputs.Interfaces
{
    public interface IInputDetector
    {
        event Action<float> OnHorizontalStateChanged;
        event Action OnInputJump;
        event Action OnReleaseJump;
        event Action<Vector2> OnDragStart;
        event Action<Vector2> OnDrag;
        event Action<Vector2> OnDragEnd;

        void ToggleActive(bool active);
    }
}