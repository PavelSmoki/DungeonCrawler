using UnityEngine;

namespace Game.Player
{
    public class PlayerModifiers
    {
        [field: Min(1f)] public float SpeedModifier { get; private set; }
        [field: Min(1f)] public float DamageModifier { get; private set; }
        [field: Min(1f)] public float CritChanceModifier { get; private set; }
        [field: Min(1f)] public float AttackSpeedModifier { get; private set; }
        [field: Min(1f)] public float AttackRangeModifier { get; private set; }
        [field: Min(1f)] public float ShotSpeedModifier { get; private set; }

        public PlayerModifiers(float speedModifier, float damageModifier, float critChanceModifier,
            float attackSpeedModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            SpeedModifier = speedModifier;  
            DamageModifier = damageModifier + 1;
            CritChanceModifier = critChanceModifier + 1;
            AttackSpeedModifier = attackSpeedModifier + 1;
            AttackRangeModifier = attackRangeModifier + 1;
            ShotSpeedModifier = shotSpeedModifier + 1;
        }
    }
}