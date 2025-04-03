using System.Collections;
using System.Threading;
using UnityEngine;
using Noong2.StatSystem;


namespace Creature
{
    public class Boss : ACreature
    {
        #region Fields
        public float idleSpeed = 10f;
        public float jumpSpeed = 10f;
        public float attackPower = 4f;
        public float attackDuration = 0.5f;
        public float fadeDuration = 1f;
        public float charmingRate = 0.05f;
        public float chamingDuration = 1f;
        public int charmingDistance = 10;
        public Transform player;

        private enum EStatus { NotMode, Fadeout, Charming, Fadein };
        private EStatus _status = EStatus.NotMode;
        private Vector2 _ahead;
        private Collider2D _player;
        #endregion

        #region MonoBehaviour Methods
        private void Awake()
        {
            base.InitCreature(attackPower, attackRange);

            _bahaviour.AddBehaviour(ECreatureStatus.Idle, OnIdle);
            _bahaviour.AddBehaviour(ECreatureStatus.Attack, OnAttack);
            _bahaviour.AddBehaviour(ECreatureStatus.Jump, OnJump);
            _bahaviour.AddBehaviour(ECreatureStatus.Charming, OnCharming);
            _bahaviour.AddBehaviour(ECreatureStatus.Damaged, OnDamaged);
            _bahaviour.AddBehaviour(ECreatureStatus.Dead, OnDead);
            _bahaviour.InitBehaviour(ECreatureStatus.Idle);

            _direction = GetDirection();
        }

        private void FixedUpdate()
        {
            int newDirection = GetDirection();
            if (newDirection != _direction)
            {
                FlipSprite();
            }
            _direction = newDirection;
            _ahead = new Vector2(_rigidbody.position.x + _direction * attackRange, _rigidbody.position.y);
            _bahaviour.Behave();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_ahead, attackRange);
        }
        #endregion

        #region Override
        public override void BeAttacked(in float point, in StatModifier modifier)
        {
            if (!_stat.HasStat("BossHP"))
            {
                Debug.Log("BossHP 가 등록되어 있지 않습니다.");
            }

            float hp = _stat.GetStatValue("BossHP");
            hp -= point;
            _currentModifier = modifier;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _stat.SetStatValue("BossHP", hp);
            }
        }

        protected override void OnIdle()
        {
            _rigidbody.linearVelocity = new Vector2(_direction * idleSpeed, _rigidbody.linearVelocityY);
            int distance = (int)Vector2.Distance(player.position, _rigidbody.position);
            if ((_player = OnCollider(_ahead)) != null)
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Attack);
            }
            else if (distance >= charmingDistance && Random.Range(0f, 1f) < charmingRate)
            {
                _bahaviour.ChangeStatus(ECreatureStatus.Charming);
                _status = EStatus.Fadeout;
            }
        }

        protected override void OnAttack()
        {
            _speed = 0;
            _animator.SetInteger("direction", 0);
            Invoke(nameof(AttackPlayer), attackDuration);
            _player = null;
            _bahaviour.ChangeStatus(ECreatureStatus.Jump);
        }

        protected override void OnJump()
        {
            CancelInvoke(nameof(AttackPlayer));
            if (!_animator.GetBool("IsJump") && IsOnGround())
            {
                _animator.SetBool("IsJump", true);
                Vector2 direction = new Vector2(-_direction, 1) * jumpSpeed;
                _rigidbody.linearVelocity = direction;
            }
            else if (_rigidbody.linearVelocity.normalized.y < 0 && IsOnGround() && _animator.GetBool("IsJump"))
            {
                _animator.SetBool("IsJump", false);
                _animator.SetInteger("direction", GetDirection());
                _bahaviour.ChangeStatus(ECreatureStatus.Idle);
            }
        }

        protected override void OnCharming()
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _animator.SetInteger("direction", 0);
            switch (_status)
            {
                case EStatus.Fadeout:
                    StartCoroutine(Fade(0));
                    break;
                case EStatus.Charming:
                    CharmingAttack();
                    break;
                case EStatus.Fadein:
                    StartCoroutine(Fade(1));
                    _animator.SetInteger("direction", GetDirection());
                    _bahaviour.ChangeStatus(ECreatureStatus.Idle);
                    break;
            }
        }

        protected override void OnDead()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        private int GetDirection()
        {
            return (player.position.x - _rigidbody.position.x) < 0 ? -1 : 1;
        }
        private void AttackPlayer()
        {
            if (_player == null)
            {
                return;
            }
            else if (_player.gameObject.TryGetComponent<TestWarrior>(out var warrior))
            {
                warrior.BeAttacked(attackPower, currentModifier);
            }
        }
        private IEnumerator Fade(float targetAlpha)
        {
            Color color = _spriteRenderer.color;
            float start = color.a;

            for (float time = 0; time < 1f; time += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(start, targetAlpha, time / fadeDuration);
                color.a = alpha;
                _spriteRenderer.color = color;
                yield return null;
            }
            _status = targetAlpha == 0 ? EStatus.Charming : EStatus.Fadeout;
        }

        private void CharmingAttack()
        {
            _rigidbody.MovePosition(new Vector2(player.position.x - (_direction * attackRange), _rigidbody.position.y));
            if ((_player = OnCollider(_ahead)) != null)
            {
                Invoke(nameof(AttackPlayer), chamingDuration);
            }
            _player = null;
            _status = EStatus.Fadein;
        }
        #endregion
    }
}
