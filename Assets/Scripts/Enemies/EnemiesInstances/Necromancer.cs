using Game.Gameplay.Audio;
using UnityEngine;

namespace Game.Enemies.EnemiesInstances
{
    public class Necromancer : EnemyBase
    {
        [SerializeField] private EnemyAttackSound _attackSound;
        [SerializeField] private EnemyProjectile _projectilePrefab;
        [SerializeField] private float _shotSpeed;

        private void Attack()
        {
            if (IsDelayed)
            {
                var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

                var direction = Player.GetCurrentPosition() - (Vector2)transform.position;
                var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                projectile.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
                projectile.GetComponent<EnemyProjectile>()
                    .SetProjectileFields(projectile.transform.up * _shotSpeed);

                _attackSound.PlayAttack();
            }
        }
    }
}