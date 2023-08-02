using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class RangedWeapon : WeaponBase
    {
        [field: SerializeField] public float ShotSpeed { get; protected set; }

        [SerializeField] private GameObject _projectilePrefab;

        public override EnemyBase Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;
            var shotSpeed = ShotSpeed * shotSpeedModifier;

            var projectileLifeTime = attackRange / shotSpeed;

            var isCrit = IsCrit(critChance, ref damage);

            var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.rotation = transform.rotation;
            projectile.GetComponent<Projectile>().SetProjectileFields(projectileLifeTime, damage,
                projectile.transform.up * shotSpeed, isCrit);
            return null;
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
                new("AttackRange", AttackRange),
                new("ShotSpeed", ShotSpeed)
            };
        }
    }
}