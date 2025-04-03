using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Noong2.StatSystem;


namespace Creature
{
    /// <summary>
    /// 테스트용 크리처 Frog
    /// WaitingTime만큼 가만히 있다가 Jump 합니다.
    /// </summary>
    public class Frog : ACreature
    {
        #region Fields
        public float jumpSpeed = 3f;
        public float waitingTime = 3f;
        #endregion

        #region MonoBehaviour Methods
        private void Awake()
        {
            base.InitCreature(1f, 1f);

            _bahaviour.AddBehaviour(ECreatureStatus.Idle, OnIdle);
            _bahaviour.AddBehaviour(ECreatureStatus.Air, OnAir);
            _bahaviour.AddBehaviour(ECreatureStatus.Jump, OnJump);
            _bahaviour.InitBehaviour(ECreatureStatus.Idle);
        }

        private void FixedUpdate()
        {
            FlipSprite();
            _bahaviour.Behave();
        }
        #endregion

        #region Override
        protected override void OnIdle()
        {
            if (!IsOnGround())
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Air);
            }
            Invoke(nameof(ChangeToJumpMode), waitingTime);
        }

        protected override void OnAir()
        {
            _animator.SetBool("OnAir", true);
            if (IsOnGround())
            {
                _animator.SetBool("OnAir", false);
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }
        }

        protected override void OnJump()
        {
            CancelInvoke(nameof(ChangeToJumpMode));
            if (!_animator.GetBool("IsJump") && IsOnGround())
            {
                _animator.SetBool("IsJump", true);
                Vector2 direction = new Vector2(_direction * 0.5f, 2) * jumpSpeed;
                _rigidbody.linearVelocity = direction;
            }
            else if (_rigidbody.linearVelocity.normalized.y < 0 && IsOnGround() && _animator.GetBool("IsJump"))
            {
                _animator.SetBool("IsJump", false);
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }
        }

        public override void BeAttacked(in float point, in StatModifier modifier)
        {
            float hp = _stat.GetStatValue("FrogHP");
            hp -= point;
            _currentModifier = modifier;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _stat.SetStatValue("FrogHP", hp);
            }
        }

        protected override void FlipSprite()
        {
            Vector2 ahead = Vector2.right * _direction;
            Collider2D collider = OnCollider(ahead);

            if (collider != null)
            {
                _direction *= -1;
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                _rigidbody.linearVelocityX *= _direction;
            }
        }
        #endregion

        #region Private Methods
        private void ChangeToJumpMode()
        {
            _bahaviour.ChangeStatus(ECreatureStatus.Jump);
        }
        #endregion
    }
}
