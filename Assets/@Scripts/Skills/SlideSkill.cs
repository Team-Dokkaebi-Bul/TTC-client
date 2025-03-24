using UnityEngine;
using System.Collections;

public class SlideSkill : BaseSkill
{
    #region Fields
    [SerializeField] private float _slideSpeed = 15f;
    [SerializeField] private float _slideDistance = 5f;
    [SerializeField] private float _groundCheckDistance = 2f;
    [SerializeField] private LayerMask _groundLayer;

    private static readonly int IsSliding = Animator.StringToHash("isSliding");
    private CapsuleCollider2D _collider;
    private float _originalColliderHeight;
    private TestController _playerController;
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Public Methods
    public override void Enable(TestWarrior owner)
    {
        base.Enable(owner);
        _playerController = owner.GetComponent<TestController>();
        _collider = owner.GetComponent<CapsuleCollider2D>();

        if (_collider == null)
        {
            Debug.LogError("플레이어에서 CapsuleCollider2D를 찾을 수 없습니다!");
            return;
        }

        _originalColliderHeight = _collider.size.y;

        if (_playerController == null)
        {
            Debug.LogError("TestController를 찾을 수 없습니다!");
        }
    }

    public override void Execute()
    {
        if (!_isEnabled) return;
        if (!CanUse) return;
        if (_collider == null) return;

        _isExecuting = true;
        _currentCooldown = _cooldownTime;
        _animator.SetTrigger(IsSliding);

        StartCoroutine(SlideRoutine());
    }
    #endregion

    #region Private Methods
    private void KeepAboveGround()
    {
        Vector2 rayStart = _ownerTransform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, _groundCheckDistance, _groundLayer);

        if (hit.collider != null)
        {
            float desiredY = hit.point.y + (_collider.size.y * 0.5f);
            _ownerTransform.position = new Vector2(_ownerTransform.position.x, desiredY);
        }
    }

    private IEnumerator SlideRoutine()
    {
        _collider.size = new Vector2(
            _collider.size.x,
            _originalColliderHeight * 0.5f
        );

        float direction = Mathf.Sign(_playerController.transform.localScale.x);
        Vector2 startPosition = _ownerTransform.position;
        float distanceMoved = 0f;
        float originalYVelocity = _rb.linearVelocity.y;

        while (distanceMoved < _slideDistance)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                _ownerTransform.position,
                new Vector2(_collider.size.x * 0.8f, _collider.size.y * 0.8f),
                0f,
                new Vector2(direction, 0),
                _slideSpeed * Time.deltaTime,
                _groundLayer
            );

            if (hit.collider != null)
            {
                break;
            }

            Vector2 newPosition = _ownerTransform.position;
            newPosition.x += direction * _slideSpeed * Time.deltaTime;
            _ownerTransform.position = newPosition;

            KeepAboveGround();
            distanceMoved = Mathf.Abs(_ownerTransform.position.x - startPosition.x);
            _rb.linearVelocity = Vector2.zero;

            yield return null;
        }

        _collider.size = new Vector2(
            _collider.size.x,
            _originalColliderHeight
        );

        _isExecuting = false;
    }
    #endregion
}