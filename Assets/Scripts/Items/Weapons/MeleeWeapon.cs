using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        public override void Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;

            var mask = LayerMask.GetMask(EnemyLayerName, FlyableEnemyLayerName);
            var hits = Physics2D.CircleCastAll(transform.position, attackRange,
                Vector2.one, 0, mask);

            foreach (var hit in hits)
            {
                var angle = Vector2.Dot(transform.up,
                    (hit.point - new Vector2(transform.position.x, transform.position.y)).normalized);
                if (angle >= 0.2f)
                {
                    Debug.Log($"Hit {hit.transform.gameObject.name}");
                    
                    if (Random.Range(0f, 1f) <= critChance) damage *= CritModifier;
                    
                    hit.transform.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
                }
            }
        }
    }
}