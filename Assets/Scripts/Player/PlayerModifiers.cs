namespace Game.Player
{
    public class PlayerStats
    {
        public float SpeedModifier { get; private set; } = 1;
        public float DamageModifier { get; private set; } = 1;
        public float CritChanceModifier { get; private set; }
        public float AttackSpeedModifier { get; private set; }
        public float AttackRangeModifier { get; private set; }
        public float ShotSpeedModifier { get; private set; }

        public PlayerStats(float speedModifier, float damageModifier, float critChanceModifier, float attackSpeedModifier, float attackRangeModifier,
            float shotSpeedModifier)
        {
            SpeedModifier = speedModifier;
            DamageModifier = damageModifier;
            CritChanceModifier = critChanceModifier;
            AttackSpeedModifier = attackSpeedModifier;
            AttackRangeModifier = attackRangeModifier;
            ShotSpeedModifier = shotSpeedModifier;
        }
    }
}