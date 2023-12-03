using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.Inputs.Interfaces;
using OTBG.Gameplay.Player.Interfaces;
using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace OTBG.Gameplay.Player.Movement
{
    [RequireComponent(typeof(WallChecker))]
    public class PlayerMovement : MonoBehaviour, IDeathHandler, IEnvironmentModifiable
    {
        public event Action OnIdle;
        public event Action<int> OnMovement;
        public event Action<bool> OnGrounded;
        public event Action<float> OnYVelocityChange;

        [SerializeField, FoldoutGroup("Movement")] private float _acceleration = 2f;
        [SerializeField, FoldoutGroup("Movement")] private float _deceleration = 0.9f;
        [SerializeField, FoldoutGroup("Movement")] private float _maxSpeed = 5f;
        [SerializeField, FoldoutGroup("Movement")] private float _maxFallSpeed = 50f;
        [SerializeField, FoldoutGroup("Movement")] private float _movementResistance = 1f;

        [SerializeField, FoldoutGroup("Ground Check")] private Transform _groundCheckPosition;
        [SerializeField, FoldoutGroup("Ground Check")] private LayerMask _groundCheckLayers;
        [SerializeField, FoldoutGroup("Ground Check")] private float _groundCheckRadius;

        [SerializeField, FoldoutGroup("Wall Stats")] private float _normalGravity;
        [SerializeField, FoldoutGroup("Wall Stats")] private float _wallGravity;
        [SerializeField, FoldoutGroup("Wall Stats")] private float _loseControlTime;

        [FoldoutGroup("Pause Movement Values"), SerializeField] private float _pauseControlAcceleration = 0.5f;
        [FoldoutGroup("Pause Movement Values"), SerializeField] private float _pauseControlTimer = 2f;
        [FoldoutGroup("Pause Movement Values"), SerializeField] private float _pauseControlReturnSpeed = 0.1f;

        private IInputDetector _inputDetector;
        private Rigidbody2D _rb;
        private WallChecker _wallChecker;

        private float _originalAcceleration;
        private float _targetSpeed = 0f;
        private bool _lastGroundedState;
        private bool _isOverridden;
        private bool _isControlPaused;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _wallChecker = GetComponent<WallChecker>();
            _inputDetector = UtilityFuncs.FindInterfaceInScene<IInputDetector>();

            if (_inputDetector != null)
            {
                _inputDetector.OnHorizontalStateChanged += OnHorizontalStateChanged;
            }
        }

        private void Start()
        {
            _originalAcceleration = _acceleration;
        }

        public void OnHorizontalStateChanged(float state)
        {
            if (_isOverridden)
                state = 0;

            _targetSpeed = state * _maxSpeed;
            if (state == 0)
                return;

            OnMovement?.Invoke(Mathf.RoundToInt(state));
        }

        private void Update()
        {
            WallDescendingCheck();
        }

        void FixedUpdate()
        {
            Movement();
            CapFallSpeed();
            ApplyMovementResistance();
            OnYVelocityChange?.Invoke(_rb.velocity.y);
        }

        private void WallDescendingCheck()
        {
            _rb.gravityScale = IsDescendingWall() ? _wallGravity : _normalGravity;
        }

        private void Movement()
        {
            if(_isControlPaused) return;

            if (Mathf.Approximately(_targetSpeed, 0f))
            {
                // Apply deceleration when no input is given
                float newVelocityX = _rb.velocity.x * _deceleration;
                _rb.velocity = new Vector2(newVelocityX, _rb.velocity.y);

                if (Mathf.Abs(newVelocityX) < 0.1f)  // Threshold to consider it idle
                {
                    _rb.velocity = new Vector2(0f, _rb.velocity.y);
                    OnIdle?.Invoke();
                }
            }
            else
            {
                // Apply acceleration toward the target speed
                float newVelocityX = Mathf.MoveTowards(_rb.velocity.x, _targetSpeed, _acceleration * Time.fixedDeltaTime);
                _rb.velocity = new Vector2(newVelocityX, _rb.velocity.y);
            }

            bool isGrounded = IsGrounded();

            if (_lastGroundedState != isGrounded)
            {
                OnGrounded?.Invoke(isGrounded);
                _lastGroundedState = isGrounded;
            }
        }

        private void CapFallSpeed()
        {
            if (_rb.velocity.y > 0)
                return;

            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_maxFallSpeed));
        }
        private void ApplyMovementResistance()
        {
            if (_movementResistance <= 0f) return;  // Guard clause to prevent invalid operation

            _rb.velocity /= _movementResistance;
        }
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _groundCheckLayers); // Placeholder
        }

        public bool IsDescendingWall()
        {
            if (IsGrounded()) return false;
            if (!_wallChecker.IsMovingIntoWall()) return false;
            if (!_wallChecker.IsNextToAnyWall()) return false;
            if (_rb.velocity.y > 0f) return false;
            return true;
        }

        public void OnDeath() 
        {
            _isOverridden = true;
           // _rb.isKinematic = true;
            _rb.simulated = false;
        }
        public void OnRevive()
        {
            _isOverridden = false;
            _rb.isKinematic = false;
            _rb.simulated = true;
        }

        // Private Coroutine reference
        private Coroutine _pauseControlCoroutine;

        // Coroutine to pause control
        public IEnumerator PauseControl()
        {

            float elapsedTime = 0f;
            float duration = _pauseControlTimer;

            // Set acceleration to the lower value
            _acceleration = _pauseControlAcceleration;

            while (elapsedTime < duration)
            {
                // Lerp acceleration from the lower value back to the original value
                _acceleration = Mathf.Lerp(_pauseControlAcceleration, _originalAcceleration, elapsedTime / duration * _pauseControlReturnSpeed);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset to the original value
            _acceleration = _originalAcceleration;
        }

        // Method to start the coroutine, stopping any existing one
        public void StartPauseControl()
        {
            // Stop the existing coroutine if it's already running
            if (_pauseControlCoroutine != null)
            {
                StopCoroutine(_pauseControlCoroutine);
            }

            // Start the new coroutine and store its reference
            _pauseControlCoroutine = StartCoroutine(PauseControl());
        }

        Coroutine _forceThrowCoroutine;
        public void TriggerForceThrow(Vector2 direction, float force, float timer = 0)
        {
            if(_forceThrowCoroutine != null)
            {
                StopCoroutine(_forceThrowCoroutine);
                _forceThrowCoroutine = null;
            }
            _forceThrowCoroutine = StartCoroutine(ForceThrow(direction, force, timer));
        }

        public IEnumerator ForceThrow(Vector2 direction, float force, float timer = 0)
        {
            _isControlPaused = true;
            _normalGravity = 2;
            _rb.AddForce(direction * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(timer);
            _normalGravity = 4;
            _isControlPaused = false;
        }

        public void ForceStop()
        {
            _rb.velocity = Vector3.zero;
        }

        void OnDestroy()
        {
            if (_inputDetector != null)
            {
                _inputDetector.OnHorizontalStateChanged -= OnHorizontalStateChanged;
            }
        }

        private void OnDrawGizmos()
        {
            if (_groundCheckPosition == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheckPosition.position, _groundCheckRadius);
        }

        public Rigidbody2D GetRB() => _rb;

        public void ApplyModifier(MovementModifier modifier)
        {
            _originalAcceleration = modifier.acceleration;
            _acceleration = modifier.acceleration;
            _deceleration = modifier.deceleration;
            _movementResistance = modifier.resistance;
            _normalGravity = modifier.gravity;
            _maxFallSpeed = modifier.maxFallSpeed;
        }

        internal void ForceUpdate()
        {
            throw new NotImplementedException();
        }
    }
}