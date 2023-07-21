using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float _ammoLifeTime;
        private float _damage;
        private CancellationTokenSource _cancellationTokenSource;

        public void SetAmmoFields(float ammoLifeTime, float damage)
        {
            _ammoLifeTime = ammoLifeTime;
            _damage = damage;
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
            if (other.gameObject.CompareTag("Enemy"))
                other.gameObject.GetComponent<EnemyBase>().TakeDamage(_damage);
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }
    }
}