using UnityEngine;
using System.Collections;

public class DashSkill : BaseSkill
{
    #region Fields
    [SerializeField] private float _dashSpeed = 30f;
    [SerializeField] private float _dashDistance = 5f;

    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private TestController _playerController;
    private CapsuleCollider2D _collider;
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
        _animator.SetTrigger(IsDashing);

        StartCoroutine(DashRoutine());
    }
    #endregion

    #region Private Methods
    private IEnumerator DashRoutine()
    {
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0;

        float direction = Mathf.Sign(_playerController.transform.localScale.x);

        Vector2 startPosition = _ownerTransform.position;
        float distanceMoved = 0f;

        float colliderHeight = _collider.size.y;
        float colliderWidth = _collider.size.x;

        while (distanceMoved < _dashDistance)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                _ownerTransform.position,
                new Vector2(colliderWidth * 0.8f, colliderHeight * 0.8f),
                0f,
                new Vector2(direction, 0),
                _dashSpeed * Time.deltaTime,
                LayerMask.GetMask("Ground")
            );

            if (hit.collider != null)
            {
                break;
            }

            Vector2 newPosition = _ownerTransform.position;
            newPosition.x += direction * _dashSpeed * Time.deltaTime;
            _ownerTransform.position = newPosition;

            distanceMoved = Mathf.Abs(_ownerTransform.position.x - startPosition.x);
            _rb.linearVelocity = new Vector2(0, 0);

            yield return null;
        }

        _rb.gravityScale = originalGravity;
        _isExecuting = false;
    }
    #endregion
}