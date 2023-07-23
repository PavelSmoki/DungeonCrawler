using System;
using Cysharp.Threading.Tasks;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rb;
        
        [field: SerializeField] protected float Speed { get; set; }
        [field: SerializeField] protected float Health { get; set; }
        // [field: SerializeField] public GameObject Particle { get; private set; }
        
        protected IPlayer Player;
        protected bool IsDelayed { get; private set; }

        [Inject]
        private void Construct(IPlayer player)
        {
            Player = player;
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

        public virtual void TakeDamage(float damage, Vector2 knockbackDirection, float knockBack)
        {
            _rb.AddForce(knockbackDirection * knockBack, ForceMode2D.Impulse);
            
            Health -= damage;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}