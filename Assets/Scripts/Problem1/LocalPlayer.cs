using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DecodeType
{
    OnlyX,
    OnlyY,
    OnlyZ,
    XandY,
    YandZ,
    ZandX,
    XYZ
}

public class LocalPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private ClientPlayer client;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private Gun gun;
    
    [SerializeField , Range(0,20)] private float speed ;
    [SerializeField , Range(0,150)] private int maxAcceleration;
    [SerializeField , Range(0,100)] private int maxAirAcceleration;
    [SerializeField , Range(0 , 20)] private int maxJumpHeight;
    [SerializeField , Range(0 , 5)] private int maxAirJumps;
    
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
    private Vector3 _desiredVelocity;



    private void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.PlayerAction.Enable();
        _input.PlayerAction.Movement.performed += OnMovement;
    }
    

    private void OnDisable()
    {
        _input.PlayerAction.Disable();
        _input.PlayerAction.Movement.performed -= OnMovement;
    }

    void Update()
    {
        
        // Debug.LogFormat("[Update] a {0} b {1} a &= b {2} " , a , b , a &= b);
        // _playerInput.x = Input.GetAxis("Horizontal");
        // _playerInput.y = Input.GetAxis("Vertical");
        //
        // _desiredJump |= Input.GetButtonDown("Jump");
        _playerInput =_input.PlayerAction.Movement.ReadValue<Vector2>();
        _desiredJump |= _input.PlayerAction.Jump.triggered;  

        _isMovementKeysPressed = _playerInput.x != 0 || _playerInput.y != 0;

        if (_input.PlayerAction.Shoot.IsPressed())
        {
            gun.Shoot();
        }
        // _desiredJump = !_desiredJump;

        _playerInput = Vector2.ClampMagnitude(_playerInput, 1f);
        
        _desiredVelocity = new Vector3(_playerInput.x, 0, _playerInput.y) * (speed * Time.deltaTime);

        // _velocity += desiredVelocity * Time.deltaTime;

        
        // characterController.Move(_desiredVelocity);
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
        return;
        // var maxSpeedChange = _isGrounded ? maxAcceleration : maxAirAcceleration * Time.deltaTime;
        //
        // _velocity = rigidBody.velocity;
        // _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
        // _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);
        //  
        // // var displacement = _velocity * Time.deltaTime;
        // // transform.localPosition += displacement;
        //
        // if (_desiredJump)
        // {
        //     _desiredJump = false;
        //     Jump();
        // }
        // UpdateVelocity();
        //
        // var lookDirection = new Vector3(_velocity.x, 0, _velocity.z);
        //
        // transform.rotation = Quaternion.LookRotation(lookDirection);
        // _isGrounded = false;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        // _playerInput.x = value.ReadValue<Vector2>().x;
        // _playerInput.y = value.ReadValue<Vector2>().y;
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
        
        var verticalVelocity = Mathf.Sqrt( -2f * Physics.gravity.y * maxJumpHeight);

        if (_velocity.y > 0)
        {
            verticalVelocity = math.max(verticalVelocity - _velocity.y, 0);
        }
        _velocity.y += verticalVelocity;
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        var jumpable = collisionInfo.gameObject.GetComponent<Jumpable>();
        if (jumpable)
        {
            _isGrounded = true;
        }
    }

    private void HandleSendingData()
    {
        var position = transform.position;

        var xShort = (short)Math.Round(position.x);
        var yShort = (short)Math.Round(position.y);
        var zShort = (short)Math.Round(position.z);

        if (GetDecodeType(position, out var currentDecodeType)) return;

        switch (currentDecodeType)
        {
            case DecodeType.OnlyX:
                client.UpdatePosition((byte)currentDecodeType, xShort); // min  1 + 2 = 3 bytes
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short));
                break;
            case DecodeType.OnlyY:
                client.UpdatePosition((byte)currentDecodeType, yShort);
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short));
                break;
            case DecodeType.OnlyZ:
                client.UpdatePosition((byte)currentDecodeType, zShort);
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short));
                break;
            case DecodeType.XandY:
                client.UpdatePosition((byte)currentDecodeType, xShort, yShort);
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short) * 2);
                break;
            case DecodeType.YandZ:
                client.UpdatePosition((byte)currentDecodeType, yShort, zShort);
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short) * 2);
                break;
            case DecodeType.ZandX:
                client.UpdatePosition((byte)currentDecodeType, zShort, xShort);
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short) * 2);
                break;
            case DecodeType.XYZ:
                client.UpdatePosition((byte)currentDecodeType, xShort, yShort, zShort); // max 1 + 2 + 2 + 2 = 7 bytes
                Debug.LogFormat(" sent  position :  {0} | size :  {1} ", position, sizeof(byte) + sizeof(short) * 3);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        _lastSentXValue = position.x;
        _lastSentYValue = position.y;
        _lastSentZValue = position.z;
    }
    private bool GetDecodeType(Vector3 position, out DecodeType currentDecodeType)
    {
        currentDecodeType = DecodeType.OnlyX;

        if (Math.Abs(_lastSentXValue - position.x) < _floatCheckTolerance)
        {
            if (Math.Abs(_lastSentYValue - position.y) > _floatCheckTolerance)
            {
                currentDecodeType = DecodeType.OnlyY;
                if (Math.Abs(_lastSentZValue - position.z) > _floatCheckTolerance)
                {
                    currentDecodeType = DecodeType.YandZ;
                }
            }
            else
            {
                currentDecodeType = DecodeType.OnlyZ;
                if (Math.Abs(_lastSentZValue - position.z) < _floatCheckTolerance)
                {
                    return true;
                }
            }
        }
        else
        {
            if (Math.Abs(_lastSentYValue - position.y) > _floatCheckTolerance)
            {
                currentDecodeType = DecodeType.XandY;
                if (Math.Abs(_lastSentZValue - position.z) > _floatCheckTolerance)
                {
                    currentDecodeType = DecodeType.XYZ;
                }
            }
            else
            {
                currentDecodeType = DecodeType.ZandX;
                if (Math.Abs(_lastSentZValue - position.z) < _floatCheckTolerance)
                {
                    currentDecodeType = DecodeType.OnlyX;
                }
            }
        }

        return false;
    }
}