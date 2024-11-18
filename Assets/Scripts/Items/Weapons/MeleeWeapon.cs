using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Game.Items.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        [SerializeField] protected float KnockBack;

        public override List<EnemyBase> Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            base.Attack(damageModifier, critChanceModifier, attackRangeModifier, shotSpeedModifier);
            
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;

            var mask = LayerMask.GetMask(EnemyLayerName, FlyableEnemyLayerName);

            var hits = Physics2D.CircleCastAll(transform.position, attackRange, 
            Vector2.zero, 0, mask);
            var enemies = new List<EnemyBase>();
            
            foreach (var hit in hits)
            {
                var angle = Vector2.Dot(transform.up,
                    (hit.point - new Vector2(transform.position.x, transform.position.y)).normalized);
                if (angle >= 0.2f)
                {
                    var enemy = hit.transform.gameObject;

                    var knockbackDirection = hit.transform.position - transform.position;
                    var isCrit = IsCrit(critChance, ref damage);

                    var enemyBase = enemy.GetComponent<EnemyBase>();
                    enemyBase.TakeDamage(damage, knockbackDirection, KnockBack, isCrit);
                    enemies.Add(enemyBase);
                }
            }

            return enemies;
        }

        private bool IsCrit(float critChance, ref float damage)
        {
            if (Random.Range(0f, 1f) <= critChance)
            {
                damage *= CritModifier;
                return true;
            }

            return false;
        }

        protected override void Awake()
        {
            Infos = new List<ItemInfo>
            {
                new("Damage", Damage),
                new("CritChance", CritChance),
                new("CritModifier", CritModifier),
                new("AttackSpeed", AttackSpeed),
                new("AttackRange", AttackRange)
            };
        }
    }
}