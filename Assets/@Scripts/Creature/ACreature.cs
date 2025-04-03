using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Noong2.StatSystem;


namespace Creature
{
    /// <summary>
    /// 크리처의 기본 뼈대가 되는 클래스입니다.
    /// 크리처를 생성할 때, 해당 클래스를 상속받아 생성합니다.
    /// </summary>
    public abstract class ACreature : MonoBehaviour
    {
        #region Fields
        protected int _direction = -1;
        protected float _speed;

        protected Animator _animator;
        protected SpriteRenderer _spriteRenderer;
        protected Rigidbody2D _rigidbody;

        protected StatModifier _currentModifier;

        public float damage;
        public float attackRange;
        public bool isAttack = false;

        // StatComponent 참조
        [SerializeField] protected StatComponent _stat;

        public StatModifier currentModifier => _currentModifier;
        #endregion

        #region Components
        protected BehaviourComponenet _bahaviour;
        #endregion

        #region Methods
        protected void InitCreature(in float damage, in float attackRange = 0f)
        {
            if (_stat == null)
            {
                _stat = GetComponent<StatComponent>();
                if (_stat == null)
                {
                    _stat = gameObject.AddComponent<StatComponent>();
                }
            }
            
            _bahaviour = gameObject.AddComponent<BehaviourComponenet>();
            
            this.damage = damage;
            this.attackRange = attackRange;

            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _animator.SetInteger("direction", _direction);
        }

        protected bool IsOnGround()
        {
            Collider2D collider = GetHit(_rigidbody.position, Vector2.down, _spriteRenderer.bounds.size.y / 2, LayerMask.GetMask("Ground"), Color.green);
            return collider != null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !isAttack)
            {
                if (collision.gameObject.TryGetComponent<TestWarrior>(out var warrior))
                {
                    warrior.BeAttacked(damage, _currentModifier);
                    isAttack = true;
                }
            }
            else if (!collision.gameObject.CompareTag("Ground"))
            {
                FlipSprite();
            }
        }

        public Collider2D OnCollider(Vector2 position)
        {
            Collider2D collider = Physics2D.OverlapCircle(position, attackRange, LayerMask.GetMask("Player"));
            return collider;
        }
        #endregion

        #region Static Methods
        static protected Collider2D GetHit(in Vector2 start, in Vector2 dir, in float len, in int layer, in Color color)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(start, dir, len, layer);

            Debug.DrawRay(start, dir * len, color);
            return rayHit.collider;
        }
        #endregion

        #region Abstract Methods
        abstract public void BeAttacked(in float point, in StatModifier modifier);
        #endregion

        #region Virtual Methods
        virtual protected void FlipSprite()
        {
            _direction *= -1;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            _animator.SetInteger("direction", _direction);
        }

        virtual protected void OnStart() { }
        virtual protected void OnIdle() { }
        virtual protected void OnAir() { }
        virtual protected void OnJump() { }
        virtual protected void OnAttack() { }
        virtual protected void OnStop() { }
        virtual protected void OnCharming() { }
        virtual protected void OnDamaged() { }
        virtual protected void OnDead() { }
        #endregion
    }
}
