using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Gameplay.Player.Movement.Interfaces;
using OTBG.Utilities.General;
using System;
using System.Collections;
using UnityEngine;


namespace OTBG.Gameplay.Player.Movement
{
    public class GroundJump : MonoBehaviour, IJump, IEnvironmentModifiable
    {
        public int Priority => 1;

        [SerializeField] private float _initialJumpForce = 5f;
        [SerializeField] private float _coyoteTime = 0.2f;
        [SerializeField] private float _jumpBuffer = 0.1f;

        private IInputDetector _inputDetector;

        private PlayerJump _playerJump;
        private Rigidbody2D _rb;
        private Coroutine _coyoteTimeCoroutine;
        private Coroutine _jumpBufferCoroutine;

        private bool _isGrounded;
        private bool _isJumpActive;
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();

            _inputDetector.OnInputJump += ActivateJumpBuffer;
        }

        private void OnDisable()
        {
            _inputDetector.OnInputJump -= ActivateJumpBuffer;

        }

        public void Initialise(PlayerJump playerJump)
        {
            _playerJump = playerJump;
        }

        public void OnGroundedCheck(bool isGrounded)
        {

            if (isGrounded && _jumpBufferCoroutine != null)
            {
                OnJump();
                return;
            }

            if (!isGrounded && _isGrounded) // Transition from grounded to airborne
            {
                _coyoteTimeCoroutine = StartCoroutine(CoyoteTime());
            }

            _isGrounded = isGrounded;
        }

        public bool CanJump()
        {
            if (_playerJump.CanInfiniteJump) return true;
            return (_coyoteTimeCoroutine != null) || _isGrounded;
        }

        public void OnJump(Action<bool> isPausingJump = null)
        {
            _playerJump.AnnounceJump();

            _isJumpActive = true;
            _isGrounded = false;

            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.velocity = new Vector2(_rb.velocity.x, _initialJumpForce);

            StopCoroutines();

            isPausingJump?.Invoke(false);

        }

        private void ActivateJumpBuffer()
        {
            if (_jumpBufferCoroutine != null)
                StopCoroutine(_jumpBufferCoroutine);

            _jumpBufferCoroutine = StartCoroutine(JumpBuffer());
        }

        private void StopCoroutines()
        {
            StopCoyoteTime();
            if (_jumpBufferCoroutine != null)
            {
                StopCoroutine(_jumpBufferCoroutine);
                _jumpBufferCoroutine = null;
            }

        }

        public void StopCoyoteTime()
        {
            if (_coyoteTimeCoroutine != null)
            {
                StopCoroutine(_coyoteTimeCoroutine);
                _coyoteTimeCoroutine = null;
            }
        }


        private IEnumerator CoyoteTime()
        {
            yield return new WaitForSeconds(_coyoteTime);
            _coyoteTimeCoroutine = null;
        }

        private IEnumerator JumpBuffer()
        {
            yield return new WaitForSeconds(_jumpBuffer);
            _jumpBufferCoroutine = null;
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
            _initialJumpForce = modifier.groundJumpHeight;
        }
    }
}
