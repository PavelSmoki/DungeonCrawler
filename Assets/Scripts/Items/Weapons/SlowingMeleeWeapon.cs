using System.Collections.Generic;
using System.Linq;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class SlowingMeleeWeapon : MeleeWeapon
    {
        [SerializeField] private float _slownessDuration;

        public override List<EnemyBase> Attack(float damageModifier, float critChanceModifier,
            float attackRangeModifier, float shotSpeedModifier)
        {
            var enemyBase = base.Attack(damageModifier, critChanceModifier, attackRangeModifier, shotSpeedModifier);
            
            foreach (var enemy in enemyBase.Where(_ => enemyBase != null))
            {
                enemy.SlowDown(_slownessDuration).Forget();
            }

            return enemyBase;
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