using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _projectileLifeTime;
        
        private CancellationTokenSource _cancellationTokenSource;

        public void SetProjectileFields(Vector2 force)
        {
            _rb.AddForce(force, ForceMode2D.Impulse);
            _cancellationTokenSource = new CancellationTokenSource();
            Life().Forget();
        }

        private async UniTaskVoid Life()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_projectileLifeTime), cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();
            if (_cancellationTokenSource.IsCancellationRequested) return;
            Destroy(gameObject);
        }
        
        private void OnCollisionEnter2D()
        {
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }
    }
}