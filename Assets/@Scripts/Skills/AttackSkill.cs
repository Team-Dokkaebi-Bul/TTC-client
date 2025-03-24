using UnityEngine;
using System.Collections;

public class AttackSkill : BaseSkill
{
    #region Fields
    [SerializeField] private float _attackDuration = 0.5f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private LayerMask _enemyLayer;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
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
        _animator.SetTrigger(IsAttacking);

        StartCoroutine(AttackRoutine());
    }
    #endregion

    #region Private Methods
    private Vector2 GetAttackDirection()
    {
        if (_playerController != null)
        {
            float directionX = Mathf.Sign(_playerController.transform.localScale.x);
            return Vector2.right * directionX;
        }

        return Vector2.right;
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackDuration * 0.5f);

        Vector2 attackPosition = _ownerTransform.position;
        Vector2 direction = GetAttackDirection();
        attackPosition += direction * _attackRange;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition, _attackRange, _enemyLayer);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Creature.ACreature>(out var enemy))
            {
                enemy.BeAttacked(_attackDamage, null);
            }
        }

        yield return new WaitForSeconds(_attackDuration * 0.5f);
        _isExecuting = false;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        DrawAttackRange(Color.gray);
    }

    private void OnDrawGizmosSelected()
    {
        DrawAttackRange(Color.red);
    }

    private void DrawAttackRange(Color color)
    {
        if (_ownerTransform == null) return;

        Vector2 attackPosition = _ownerTransform.position;
        Vector2 direction = GetAttackDirection();
        attackPosition += direction * _attackRange;

        Gizmos.color = color;
        Gizmos.DrawWireSphere(attackPosition, _attackRange);

        color.a = 0.2f;
        Gizmos.color = color;
        Gizmos.DrawSphere(attackPosition, _attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_ownerTransform.position, attackPosition);
    }
    #endregion
}