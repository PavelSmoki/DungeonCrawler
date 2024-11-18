using Game.Gameplay.Audio;
using UnityEngine;

namespace Game.Enemies.EnemiesInstances
{
    public class Goblin : EnemyBase
    {
        [SerializeField] private EnemyAttackSound _attackSound;
        
        [SerializeField] private float _shotSpeed;
        [SerializeField] private EnemyProjectile _projectilePrefab;

        private void Attack()
        {
            if (IsDelayed)
            {
                _attackSound.PlayAttack();
                
                var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

                var direction = Player.GetCurrentPosition() - (Vector2)transform.position;
                var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                projectile.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
                projectile.GetComponent<EnemyProjectile>()
                    .SetProjectileFields(projectile.transform.up * _shotSpeed);
            }
        }
    }
}