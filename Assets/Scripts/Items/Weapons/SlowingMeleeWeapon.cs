using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class SlowingMeleeWeapon : MeleeWeapon
    {
        [SerializeField] private float slownessDuration;
        public override EnemyBase Attack(float damageModifier, float critChanceModifier, float attackRangeModifier, float shotSpeedModifier)
        {
            var enemyBase = base.Attack(damageModifier, critChanceModifier, attackRangeModifier, shotSpeedModifier);
            if (enemyBase != null)
            {
                enemyBase.SlowDown(slownessDuration).Forget();
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