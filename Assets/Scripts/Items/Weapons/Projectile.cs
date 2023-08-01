using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private const string EnemyTag = "Enemy";

        [SerializeField] private Rigidbody2D _rb;
        
        private float _ammoLifeTime;
        private float _damage;
        private bool _isCrit;
        private CancellationTokenSource _cancellationTokenSource;

        public void SetProjectileFields(float ammoLifeTime, float damage, Vector2 force, bool isCrit)
        {
            _rb.AddForce(force, ForceMode2D.Impulse);
            _ammoLifeTime = ammoLifeTime;
            _damage = damage;
            _isCrit = isCrit;
            _cancellationTokenSource = new CancellationTokenSource();
            Life().Forget();
        }

        private async UniTaskVoid Life()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_ammoLifeTime), cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();
            if (_cancellationTokenSource.IsCancellationRequested) return;
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(EnemyTag))
                other.gameObject.GetComponent<EnemyBase>().TakeDamage(_damage, Vector2.zero, 0, _isCrit);
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }
    }
}