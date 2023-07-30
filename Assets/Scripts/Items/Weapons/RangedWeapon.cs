using System.Collections.Generic;
using UnityEngine;

namespace Game.Items.Weapons
{
    public class RangedWeapon : WeaponBase
    {
        [field: SerializeField] public float ShotSpeed { get; protected set; }
        [SerializeField] private GameObject _ammoPrefab;

        public override void Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;
            var shotSpeed = ShotSpeed * shotSpeedModifier;

            var ammoLifeTime = attackRange / shotSpeed;

            if (Random.Range(0f, 1f) <= critChance) damage *= CritModifier;

            var ammo = Instantiate(_ammoPrefab, transform.position, Quaternion.identity);
            ammo.transform.rotation = transform.rotation;
            ammo.GetComponent<Projectile>().SetAmmoFields(ammoLifeTime, damage, ammo.transform.up * shotSpeed);
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