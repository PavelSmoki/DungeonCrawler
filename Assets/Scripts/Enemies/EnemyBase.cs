using System;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D Rb;
        
        [field: SerializeField] protected float Speed { get; set; }
        [field: SerializeField] protected float Health { get; set; }
        // [field: SerializeField] public GameObject Particle { get; private set; }
        
        private RoomData _roomData;

        protected IPlayer Player;
        protected bool IsDelayed { get; private set; }

        [Inject]
        private void Construct(IPlayer player)
        {
            Player = player;
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

        public virtual void TakeDamage(float damage, Vector2 knockbackDirection, float knockBack)
        {
            Rb.AddForce(knockbackDirection * knockBack, ForceMode2D.Impulse);
            
            Health -= damage;
            if (Health <= 0)
            {
                _roomData.EnemyCount.Value--;
                Destroy(gameObject);
            }
        }
    }
}