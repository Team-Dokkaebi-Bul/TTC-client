using UnityEngine;

public class TestController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _maxSpeed = 8f;

    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _deceleration = 50f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _rayLength = 2f;

    private Rigidbody2D _rb;
    private float _moveInput;
    private float _currentSpeed;
    private bool _isGrounded;
    private float _jumpBufferCounter;
    private float _coyoteCounter;
    private bool _isJumping;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // 입력 이벤트 해제
        Managers.Input.KeyAction -= HandleInput;
        // 입력 이벤트 등록
        Managers.Input.KeyAction += HandleInput;
    }

    private void Update()
    {
        CheckGround();
        HandleJumpBuffer();
        HandleCoyoteTime();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Move();
        HandleJumpPhysics();
    }

    private void HandleInput()
    {
        // 이동 입력
        _moveInput = Managers.Input.MoveInput;

        // 점프 입력
        if (Managers.Input.IsJumpPressed)
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
    }

    private void Move()
    {
        float targetSpeed = _moveInput * _maxSpeed;
        float speedDiff = targetSpeed - _currentSpeed;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, accelRate * Time.fixedDeltaTime);
        _rb.linearVelocity = new Vector2(_currentSpeed, _rb.linearVelocity.y);

        // 캐릭터 방향 전환
        if (_moveInput != 0)
        {
            transform.localScale = new Vector3(_moveInput > 0 ? 1 : -1, 1, 1);
        }
    }

    private void CheckGround()
    {
        bool wasGrounded = _isGrounded;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayLength, LayerMask.GetMask("Ground"));
        _isGrounded = hit.collider != null;

        Color rayColor = _isGrounded ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector2.down * _rayLength, rayColor);

        if (_isGrounded && !wasGrounded)
        {
            _coyoteCounter = _coyoteTime;
            _isJumping = false;
        }
    }

    private void HandleJumpBuffer()
    {
        if (_jumpBufferCounter > 0)
        {
            _jumpBufferCounter -= Time.deltaTime;

            if ((_isGrounded || _coyoteCounter > 0) && !_isJumping)
            {
                Jump();
            }
        }
    }

    private void HandleCoyoteTime()
    {
        if (!_isGrounded)
        {
            _coyoteCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        _jumpBufferCounter = 0;
        _coyoteCounter = 0;
        _isJumping = true;
    }

    private void HandleJumpPhysics()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    private void UpdateAnimations()
    {
        // 이동 애니메이션
        _animator.SetBool(IsMoving, Mathf.Abs(_moveInput) > 0.1f);

        // 점프 애니메이션
        _animator.SetBool(IsJumping, _isJumping && _rb.linearVelocity.y > 0);

        // 낙하 애니메이션
        _animator.SetBool(IsFalling, !_isGrounded && _rb.linearVelocity.y < 0);
    }
}