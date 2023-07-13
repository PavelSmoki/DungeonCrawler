using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        public override void Attack()
        {
            var mask = LayerMask.GetMask(EnemyLayerName, FlyableEnemyLayerName);
            var hits = Physics2D.CircleCastAll(transform.position, AttackRange,
                Vector2.one, 0, mask);
            foreach (var hit in hits)
            {
                var angle = Vector2.Dot(transform.up,
                    (hit.point - new Vector2(transform.position.x, transform.position.y)).normalized);
                if (angle >= 0.2f)
                {
                    hit.transform.gameObject.GetComponent<EnemyBase>().TakeDamage(Damage);
                }
            }
        }
    }
}