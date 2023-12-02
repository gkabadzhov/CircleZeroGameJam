using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.Player.Movement.Interfaces;
using System;
using UnityEngine;

namespace OTBG.Gameplay.Player.Movement
{
    public class AirJump : MonoBehaviour, IJump, IEnvironmentModifiable
    {
        public int Priority => 3;

        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private bool _canAirJump;

        private PlayerJump _playerJump;
        private Rigidbody2D _rb;
        private bool _isJumpActive;
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Initialise(PlayerJump playerJump)
        {
            _playerJump = playerJump;
        }
        public void OnGroundedCheck(bool isGrounded)
        {
            if (isGrounded)
            {
                _canAirJump = true; // Reset air jump when touching ground
            }
        }

        public bool CanJump()
        {
            return _canAirJump;
        }

        public void OnJump(Action<bool> isPausingJump)
        {
            _playerJump.AnnounceJump();

            _isJumpActive = true;

            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _canAirJump = false; // Disable air jump until next ground touch
            isPausingJump?.Invoke(false);
        }

        public void OnJumpRelease()
        {
            if (!_isJumpActive)
                return;

            if (_rb.velocity.y <= 0)
                return;
            _isJumpActive = false;


            float _yVel = _rb.velocity.y;
            _rb.velocity = new Vector2(_rb.velocity.x, _yVel * 0.4f);
        }

        public void ApplyModifier(MovementModifier modifier)
        {
            _jumpForce = modifier.doubleJumpHeight;
        }
    }
}