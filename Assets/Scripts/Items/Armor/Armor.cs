using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items.Armor
{
    [Serializable]
    public class Armor : Item
    {
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public float MoveSpeedBonus { get; private set; }
        [field: SerializeField] public float DamageBonus { get; private set; }
        [field: SerializeField] public float CritChanceBonus { get; private set; }
        [field: SerializeField] public float AttackSpeedBonus { get; private set; }
        [field: SerializeField] public float AttackRangeBonus { get; private set; }
        [field: SerializeField] public float ShotSpeedBonus { get; private set; }

        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            Infos = new List<ItemInfo>
            {
                new("MoveSpeedBonus", MoveSpeedBonus),
                new("DamageBonus", DamageBonus),
                new("CritChanceBonus", CritChanceBonus),
                new("AttackSpeedBonus", AttackSpeedBonus),
                new("AttackRangeBonus", AttackRangeBonus),
                new("ShotSpeedBonus", ShotSpeedBonus)
            };
        }
    }
}