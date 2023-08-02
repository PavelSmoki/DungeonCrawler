using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Player;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private static readonly int Damaged = Animator.StringToHash("Damaged");
        private static readonly int Died = Animator.StringToHash("Died");

        [SerializeField] protected Rigidbody2D Rb;
        [SerializeField] protected Animator Animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [field: SerializeField] protected float Speed { get; set; }
        [field: SerializeField] protected float Health { get; set; }

        protected IPlayer Player;

        private CancellationTokenSource _cancellationTokenSource;
        protected DamageUI DamageUI;
        private RoomData _roomData;
        private bool _isDead;

        protected bool IsDelayed { get; private set; }

        [Inject]
        private void Construct(IPlayer player, DamageUI damageUI)
        {
            Player = player;
            DamageUI = damageUI;
        }

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void SetCurrentRoomData(RoomData roomData)
        {
            _roomData = roomData;
        }

        private async UniTaskVoid BehaviorDelayAfterSpawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            IsDelayed = true;
        }

        protected virtual void Start()
        {
            BehaviorDelayAfterSpawn().Forget();
        }

        protected virtual void Update()
        {
            Rotate();
        }

        protected void Rotate()
        {
            var rot = transform.rotation;
            if (Player.GetCurrentPosition().x - transform.position.x < 0)
            {
                rot.y = 180;
                transform.rotation = rot;
            }
            else
            {
                rot.y = 0;
                transform.rotation = rot;
            }
        }

        public virtual void TakeDamage(float damage, Vector2 knockbackDirection, float knockBack, bool isCrit)
        {
            if (!_isDead)
            {
                DamageUI.ShowDamage((int)damage, transform.position, isCrit);

                Animator.SetTrigger(Damaged);

                Rb.AddForce(knockbackDirection * knockBack, ForceMode2D.Impulse);

                Health -= damage;

                if (Health <= 0)
                {
                    _isDead = true;
                    _roomData.EnemyCount.Value--;

                    Animator.SetTrigger(Died);

                    Speed = 0;
                    _collider.isTrigger = true;

                    DelayDestroy().Forget();
                }
            }
        }

        private async UniTaskVoid DelayDestroy()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }

        public async UniTaskVoid SlowDown(float duration)
        {
            Speed /= 2;
            _spriteRenderer.color = new Color(0.335f, 0.737f, 0.783f);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();
            if (_cancellationTokenSource.IsCancellationRequested) return;
            Speed *= 2;
            _spriteRenderer.color = Color.white;
        }
    }
}