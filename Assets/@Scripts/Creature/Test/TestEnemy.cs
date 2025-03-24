using System.Collections;
using UnityEngine;

namespace Creature
{
    public class TestEnemy : ACreature
    {
        #region Fields
        public float idleSpeed = 5f;
        public float attackSpeed = 20f;
        public float waitingTime = 1f;
        public float playerRayDistance = 3f;

        private Vector2 _playerPosition;
        #endregion

        #region MonoBehaviour Methods
        private void Awake()
        {
            base.InitCreature(damage, attackRange);
            _bahaviour.AddBehaviour(ECreatureStatus.Idle, OnIdle);
            _bahaviour.AddBehaviour(ECreatureStatus.Stop, OnStop);
            _bahaviour.AddBehaviour(ECreatureStatus.Attack, OnAttack);
            _bahaviour.AddBehaviour(ECreatureStatus.Air, OnAir);
            _bahaviour.InitBehaviour(ECreatureStatus.Idle);
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = new Vector2(_direction * _speed, _rigidbody.linearVelocityY);
            _bahaviour.Behave();
        }
        #endregion

        #region Override Methods
        protected override void OnIdle()
        {
            _speed = idleSpeed;
            if (!IsGroundAhead())
            {
                FlipSprite();
            }
            if (!IsOnGround())
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Air);
            }
            if (IsPlayerAhead())
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Stop);
            }
        }

        protected override void OnStop()
        {
            _speed = 0;
            Invoke(nameof(ChangeToAttackMode), waitingTime);
        }

        protected override void OnAir()
        {
            _speed = 0;
            if (IsOnGround())
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }
        }

        protected override void OnAttack()
        {
            CancelInvoke(nameof(ChangeToAttackMode));
            _speed = attackSpeed;
            float direction = _playerPosition.x - _rigidbody.position.x;

            if (Mathf.Sign(direction) != Mathf.Sign(_direction) ||
                Mathf.Abs(direction) < Mathf.Epsilon)
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }

            Collider2D player = OnCollider(_playerPosition);

            if (player != null)
            {
                if (player.TryGetComponent<TestWarrior>(out var warrior))
                {
                    warrior.BeAttacked(damage, currentModifier);
                }
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }
        }

        public override void BeAttacked(in float point, in StatModifier modifier)
        {
            if (!_stat.HasStat("TestEnemyHP"))
            {
                Debug.Log("TestEnemyHP 가 등록되어 있지 않습니다.");
            }

            float hp = _stat.GetStatValue("TestEnemyHP");
            hp -= point;
            _currentModifier = modifier;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _stat.SetStatValue("TestEnemyHP", hp);
            }
        }
        #endregion

        #region Private Methods
        private bool IsGroundAhead()
        {
            Vector2 front = new Vector2(_rigidbody.position.x + _direction * 0.5f, _rigidbody.position.y);
            Collider2D collider = GetHit(front, Vector2.down, 1, LayerMask.GetMask("Ground"), Color.green);
            return collider != null;
        }

        private bool IsPlayerAhead()
        {
            Vector2 ahead = Vector2.right * _direction;
            Collider2D playerCollider = GetHit(_rigidbody.position, ahead, playerRayDistance, LayerMask.GetMask("Player"), Color.blue);

            if (playerCollider)
            {
                _playerPosition = playerCollider.transform.position;
                return true;
            }
            return false;
        }

        private void ChangeToAttackMode()
        {
            _bahaviour.ChangeStatus(ECreatureStatus.Attack);
        }
        #endregion
    }
}
