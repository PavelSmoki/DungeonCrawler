using UnityEngine;

namespace Game.Player
{
    public class PlayerModifiers
    {
        public float SpeedModifier { get; private set; }
        public float DamageModifier { get; private set; }
        public float CritChanceModifier { get; private set; }
        public float AttackSpeedModifier { get; private set; }
        public float AttackRangeModifier { get; private set; }
        public float ShotSpeedModifier { get; private set; }

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