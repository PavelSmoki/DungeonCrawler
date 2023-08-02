using Game.Enemies;
using UnityEditor;
using UnityEngine;

namespace Game.Items.Weapons
{
    public abstract class WeaponBase : Item
    {
        [field: Header("STATS")]
        [field: SerializeField] public float Damage { get; protected set; }
        [field: SerializeField] public float CritChance { get; protected set; }
        [field: SerializeField] [field: Min(2f)] public float CritModifier { get; protected set; }
        [field: SerializeField] public float AttackSpeed { get; protected set; }
        [field: SerializeField] public float AttackRange { get; protected set; }

        protected const string EnemyLayerName = "Enemy";
        protected const string FlyableEnemyLayerName = "FlyableEnemy";

        public virtual EnemyBase Attack(float damageModifier, float critChanceModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            return null;
        }

        protected virtual void Awake()
        {
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(transform.position, Vector3.forward, AttackRange);
        }
#endif
    }
}