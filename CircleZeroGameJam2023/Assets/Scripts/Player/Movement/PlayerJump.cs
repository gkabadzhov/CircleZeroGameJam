using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Gameplay.Player.Interfaces;
using OTBG.Gameplay.Player.Movement.Interfaces;
using OTBG.Utilities.General;
using System;
using System.Linq;
using UnityEngine;

namespace OTBG.Gameplay.Player.Movement
{
    public class PlayerJump : MonoBehaviour, IDeathHandler, IEnvironmentModifiable
    {
        public event Action OnJump;

        private PlayerMovement _playerMovement;
        private IInputDetector _inputDetector;
        private IJump[] _jumpTypes;

        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _canInfiniteJump;

        private bool _isOverridden;

        public bool CanInfiniteJump => _canInfiniteJump;

        void Awake()
        {
            _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();
            _playerMovement= GetComponent<PlayerMovement>();
                        
            _jumpTypes = GetComponents<IJump>().OrderBy(j => j.Priority).ToArray();
            _jumpTypes.ToList().ForEach(j => j.Initialise(this));

            _inputDetector.OnInputJump += HandleJump;
            _inputDetector.OnReleaseJump += HandleReleaseJump;
            _playerMovement.OnGrounded += OnGroundedChanged;
        }

   
        private void OnGroundedChanged(bool isGrounded)
        {
            _isGrounded = isGrounded;
            foreach (var jumpType in _jumpTypes)
            {
                jumpType.OnGroundedCheck(_isGrounded);
            }
        }
        private void HandleReleaseJump()
        {
            _jumpTypes.ToList().ForEach(j => j.OnJumpRelease());
        }

        private void HandleJump()
        {
            if (_isOverridden)
                return;

            foreach (var jumpType in _jumpTypes)
            {
                if (jumpType.CanJump())
                {
                    jumpType.OnJump((isPausing) =>
                    {
                        if(isPausing)
                           _playerMovement.StartPauseControl();
                    });
                    return;
                }
            }
        }

        public void AnnounceJump()
        {
            OnJump?.Invoke();
        }

        private void OnDestroy()
        {
            _inputDetector.OnInputJump -= HandleJump;
            _playerMovement.OnGrounded -= OnGroundedChanged;
            _inputDetector.OnReleaseJump -= HandleReleaseJump;
        }

        public void OnDeath()
        {
            _isOverridden = true;
        }

        public void OnRevive()
        {
            _isOverridden = false;
        }

        public void ApplyModifier(MovementModifier modifier)
        {
            _canInfiniteJump = modifier.canInfinitelyJump;
        }
    }
}


