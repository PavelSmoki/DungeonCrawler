using System;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Player;
using Game.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D Rb;
        [SerializeField] protected TextMeshProUGUI DealedDamage;
        
        [field: SerializeField] protected float Speed { get; set; }
        [field: SerializeField] protected float Health { get; set; }
        // [field: SerializeField] public GameObject Particle { get; private set; }
        
        protected IPlayer Player;
        private DamageUI _damageUI;

        private RoomData _roomData;
        protected bool IsDelayed { get; private set; }

        [Inject]
        private void Construct(IPlayer player, DamageUI damageUI)
        {
            Player = player;
            _damageUI = damageUI;
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

        public virtual void TakeDamage(float damage, Vector2 knockbackDirection, float knockBack, bool isCrit)
        {
            _damageUI.ShowDamage((int)damage, transform.position, isCrit);
            
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