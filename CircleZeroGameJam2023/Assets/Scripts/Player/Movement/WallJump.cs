using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.Player.Movement.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTBG.Gameplay.Player.Movement
{
    [RequireComponent(typeof(WallChecker))]
    public class WallJump : MonoBehaviour, IJump, IEnvironmentModifiable
    {
        public int Priority => 2;

        [FoldoutGroup("Jump Stats"), SerializeField] private Vector2 _jumpForce;

        private WallChecker _wallChecker;
        private Rigidbody2D _rb;
        private bool _isGrounded;
        private bool _isJumpActive;
        public bool CanJump()
        {
            if (_isGrounded) return false;
            if (!_wallChecker.IsNextToAnyWall(out int ignore)) return false;

            return true;
        }

        public void Initialise(PlayerJump playerJump)
        {
            _rb = GetComponent<Rigidbody2D>();
            _wallChecker = GetComponent<WallChecker>();
        }

        public void OnGroundedCheck(bool isGrounded)
        {
            _isGrounded = isGrounded;
        }

        public void OnJump(Action<bool> isPausingJump)
        {
            if (!_wallChecker.IsNextToAnyWall(out int jumpDirection))
                return;

            _isJumpActive = true;

            JumpForce(jumpDirection);

            isPausingJump?.Invoke(true);
        }

        public void JumpForce(int direction)
        {
            _rb.velocity = new Vector2(0, 0);
            _rb.velocity = new Vector2(direction * _jumpForce.x, _jumpForce.y);
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
            _jumpForce = modifier.wallJumpForce;
        }
    }
}