using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace OTBG.Gameplay.Player.Movement
{
    public class WallChecker : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField] private Transform _leftCheck;
        [FoldoutGroup("References"), SerializeField] private Transform _rightCheck;

        [FoldoutGroup("Raycast Stats"), SerializeField] private LayerMask _checkLayer;
        [FoldoutGroup("Raycast Stats"), SerializeField] private float _checkSize;

        private IInputDetector _inputDetector;
        private float _lastDirection;

        private void Awake()
        {
            _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();
            _inputDetector.OnHorizontalStateChanged += _inputDetector_OnHorizontalStateChanged;
        }

        private void _inputDetector_OnHorizontalStateChanged(float lastDirection)
        {
            _lastDirection = lastDirection;
        }

        public bool IsNextToAnyWall(out int jumpDirection)
        {
            jumpDirection = 0;

            if (IsNextToWall(_leftCheck))
            {
                jumpDirection = 1;
                return true;
            }

            if (IsNextToWall(_rightCheck) )
            {
                jumpDirection = -1;
                return true;
            }

            return false;
        }

        public bool IsNextToAnyWall()
        {

            if (IsNextToWall(_leftCheck) || IsNextToWall(_rightCheck))
                return true;

            return false;
        }

        public bool IsNextToWall(Transform checkPoint)
        {
            return Physics2D.OverlapCircle(checkPoint.position, _checkSize, _checkLayer);
        }

        public bool IsMovingIntoWall()
        {
            if (IsNextToWall(_leftCheck) && _lastDirection < 0) return true;
            if (IsNextToWall(_rightCheck) && _lastDirection > 0) return true;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (_rightCheck)
                Gizmos.DrawWireSphere(_rightCheck.position, _checkSize);
            if (_leftCheck)
                Gizmos.DrawWireSphere(_leftCheck.position, _checkSize);
        }
    }

    
}