using UnityEngine;

namespace Game.Enemies.EnemiesInstances
{
    public class Doll : EnemyBase
    {
        private Vector3 _spawn;

        public override void TakeDamage(float damage, Vector2 knockbackDirection, float knockBack, bool isCrit)
        {
            DamagedSound.PlayDamaged();
            DamageUI.ShowDamage((int)damage, transform.position, isCrit);
        }

        protected override void Update()
        {
            
        }
    }
}
