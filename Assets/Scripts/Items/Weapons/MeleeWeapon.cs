using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Game.Items.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        [SerializeField] protected float KnockBack;
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[5];

        public override void Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;

            var mask = LayerMask.GetMask(EnemyLayerName, FlyableEnemyLayerName);

            Physics2D.CircleCastNonAlloc(transform.position, attackRange,
                Vector2.one, _hits, 0, mask);

            foreach (var hit in _hits)
            {
                var angle = Vector2.Dot(transform.up,
                    (hit.point - new Vector2(transform.position.x, transform.position.y)).normalized);
                if (angle >= 0.2f)
                {
                    if (hit.transform == null)
                    {
                        return;
                    }
                    var enemy = hit.transform.gameObject;
                    var knockbackDirection = hit.transform.position - transform.position;

                    if (Random.Range(0f, 1f) <= critChance) damage *= CritModifier;

                    enemy.GetComponent<EnemyBase>().TakeDamage(damage, knockbackDirection, KnockBack);
                }
            }
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