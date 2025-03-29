using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    #region Fields
    [SerializeField] protected float _cooldownTime;
    protected float _currentCooldown;
    protected bool _isExecuting;
    protected Animator _animator;
    protected Rigidbody2D _rb;
    protected bool _isEnabled = false;
    protected TestWarrior _owner;
    protected Transform _ownerTransform;
    #endregion

    #region Properties
    public bool CanUse => !_isExecuting && _currentCooldown <= 0;
    public float Cooldown => _currentCooldown;
    public float CooldownTime => _cooldownTime;
    #endregion

    #region Unity Methods
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }
    #endregion

    #region Public Methods
    public virtual void Enable(TestWarrior owner)
    {
        _owner = owner;
        _ownerTransform = owner.transform;
        _animator = owner.GetComponentInChildren<Animator>();
        _rb = owner.GetComponent<Rigidbody2D>();
        _isEnabled = true;
        Debug.Log($"[{gameObject.name}] 스킬 활성화됨");
    }

    public virtual void Disable()
    {
        _isEnabled = false;
        _owner = null;
        _ownerTransform = null;
        _animator = null;
        _rb = null;
        Debug.Log($"[{gameObject.name}] 스킬 비활성화됨");
    }

    public virtual void SetCooldownTime(float cooldownTime)
    {
        if (cooldownTime > 0)
        {
            _cooldownTime = cooldownTime;
        }
    }

    public abstract void Execute();
    #endregion
}