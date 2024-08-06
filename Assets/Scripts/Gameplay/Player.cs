using System;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Gun gun;

        [SerializeField, Range(0, 20)] private float speed;
        [SerializeField, Range(0, 20)] private int maxJumpHeight;
        [SerializeField, Range(0, 5)] private int maxAirJumps;

        PlayerInput _input;

        private int _jumpCount;

        private bool _desiredJump;
        private bool _isGrounded;
        private bool _isMovementKeysPressed;

        private float _verticalAxis;
        private float _horizontalAxis;

        private float _lastSentXValue;
        private float _lastSentYValue;
        private float _lastSentZValue;
        private float _floatCheckTolerance = 0.01f;

        private Vector2 _playerInput;
        private Vector3 _velocity;

        public event Action Dead;

        private void OnValidate()
        {
            if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.PlayerAction.Enable();
        }


        private void OnDisable()
        {
            _input.PlayerAction.Disable();
        }

        void Update()
        {
            _playerInput = InputManager.Instance.PlayerMovementInput();
            _desiredJump |= InputManager.Instance.IsJumpTriggered();

            _isMovementKeysPressed = _playerInput.x != 0 || _playerInput.y != 0;

            if (InputManager.Instance.IsFired())
            {
                gun.Shoot();
            }

            _playerInput = Vector2.ClampMagnitude(_playerInput, 1f);

        }


        private void FixedUpdate()
        {
            _velocity = new Vector3(_playerInput.x * speed, rigidBody.velocity.y,
                _playerInput.y * speed);

            if (_desiredJump)
            {
                _desiredJump = false;
                Jump();
            }

            UpdateVelocity();
            if (_isMovementKeysPressed)
            {
                var velocity = rigidBody.velocity;
                var lookDirection = new Vector3(velocity.x, 0, velocity.z);
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            _isGrounded = false;
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            _desiredJump |= value.started;
        }


        private void UpdateVelocity()
        {
            rigidBody.velocity = _velocity;

            if (_isGrounded)
                _jumpCount = 0;
        }

        private void Jump()
        {
            if (!_isGrounded && _jumpCount >= maxAirJumps) return;
            _jumpCount++;

            var verticalVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * maxJumpHeight);

            if (_velocity.y > 0)
            {
                verticalVelocity = math.max(verticalVelocity - _velocity.y, 0);
            }

            _velocity.y += verticalVelocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Dead?.Invoke();
            }
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            var jumpable = collisionInfo.gameObject.GetComponent<Jumpable>();
            if (jumpable)
            {
                _isGrounded = true;
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}