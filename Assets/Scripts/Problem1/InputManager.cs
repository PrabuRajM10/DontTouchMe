using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Problem1
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        PlayerInput _input;
        private bool _desiredJump;
        private bool _isFired;
        private bool _isPaused;
        private Vector3 _mousePosition;
        private Vector2 _playerInput;

        public Vector3 MousePosition => _mousePosition;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
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

        private void Update()
        {
            _playerInput =_input.PlayerAction.Movement.ReadValue<Vector2>();
            _desiredJump = _input.PlayerAction.Jump.triggered;
            _isFired = _input.PlayerAction.Shoot.IsPressed();
            _isPaused = _input.PlayerAction.Pause.triggered;
            _mousePosition = _input.PlayerAction.MouseLook.ReadValue<Vector2>();
            _mousePosition = Mouse.current.position.ReadValue();
        }

        public Vector2 PlayerMovementInput()
        {
            return _playerInput;
        }

        public bool IsJumpTriggered()
        {
            return _desiredJump;
        }

        public bool IsFired()
        {
            return _isFired;
        }

        public bool IsPaused()
        {
            return _isPaused;
        }
    }
}