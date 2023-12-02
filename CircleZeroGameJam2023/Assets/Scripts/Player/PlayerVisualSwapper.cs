using UnityEngine;

namespace OTBG.Gameplay.Player
{
    public class PlayerVisualSwapper : MonoBehaviour
    {
        public RotateWorldObjectToMouse _objectRotator;

        public Transform flipper;

        public void Awake()
        {
            _objectRotator = GetComponentInChildren<RotateWorldObjectToMouse>();

            _objectRotator.OnAimDirectionChanged += _objectRotator_OnAimDirectionChanged;
        }

        private void _objectRotator_OnAimDirectionChanged(bool isAimingRight)
        {
            flipper.localScale = new Vector3(isAimingRight ? 1:-1 , 1, 1);
        }
    }
}

