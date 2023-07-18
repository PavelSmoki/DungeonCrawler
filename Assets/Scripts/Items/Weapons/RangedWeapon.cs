using UnityEngine;

namespace Game.Items.Weapons
{
    public class RangedWeapon : WeaponBase
    {
        [SerializeField] private float _shotSpeed;
        [SerializeField] private GameObject _ammoPrefab;

        public override void Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            var damage = Damage * damageModifier;
            var critChance = CritChance * critChanceModifier;
            var attackRange = AttackRange * attackRangeModifier;
            var shotSpeed = _shotSpeed * shotSpeedModifier;

            var ammoLifeTime = attackRange / shotSpeed;

            if (Random.Range(0f, 1f) <= critChance) damage *= CritModifier;

            var ammo = Instantiate(_ammoPrefab, transform.position, Quaternion.identity);
            ammo.transform.rotation = transform.rotation;
            ammo.GetComponent<Rigidbody2D>().AddForce(ammo.transform.up * shotSpeed, ForceMode2D.Impulse);
            ammo.GetComponent<Projectile>().SetAmmoFields(ammoLifeTime, damage);
        }
    }
}