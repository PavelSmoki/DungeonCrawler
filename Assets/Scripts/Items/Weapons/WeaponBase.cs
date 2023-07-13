using Game.Player;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Items.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [field: SerializeField] protected Rareness Rareness { get; set; }
        [field: SerializeField] protected float Damage { get; set; }
        [field: SerializeField] protected float CritChance { get; set; }
        [field: SerializeField] protected float AttackSpeed { get; set; }
        [field: SerializeField] protected float AttackRange { get; set; }

        protected const string EnemyLayerName = "Enemy";
        protected const string FlyableEnemyLayerName = "FlyableEnemy";

        public virtual void Attack() {}

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(transform.position, Vector3.forward, AttackRange);
        }
#endif
    }
}