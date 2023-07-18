using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float AmmoLifeTime;
        private float Damage;
        private CancellationTokenSource _cancellationTokenSource;

        public void SetAmmoFields(float ammoLifeTime, float damage)
        {
            AmmoLifeTime = ammoLifeTime;
            Damage = damage;
            _cancellationTokenSource = new ();
            Life().Forget();
        }

        private async UniTaskVoid Life()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(AmmoLifeTime), cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();
            if (_cancellationTokenSource.IsCancellationRequested) return;
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
                other.gameObject.GetComponent<EnemyBase>().TakeDamage(Damage);
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }
    }
}