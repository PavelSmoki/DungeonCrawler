using System;
using Game.Items.Weapons;
using UnityEngine;

namespace Game.Items.Armor
{
    [Serializable]
    public class Armor : Item
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Rareness Rareness { get; private set; }
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public float MoveSpeedBonus { get; private set; }
        [field: SerializeField] public float DamageBonus { get; private set; }
        [field: SerializeField] public float CritChanceBonus { get; private set; }
        [field: SerializeField] public float AttackSpeedBonus { get; private set; }
        [field: SerializeField] public float AttackRangeBonus { get; private set; }
        [field: SerializeField] public float ShotSpeedBonus { get; private set; }

        // private void Awake()
        // {
        //     Infos.Add(new ItemInfo("MoveSpeedBonus", MoveSpeedBonus));
        //     Infos.Add(new ItemInfo("DamageBonus", DamageBonus));
        //     Infos.Add(new ItemInfo("CritChanceBonus", CritChanceBonus));
        //     Infos.Add(new ItemInfo("AttackSpeedBonus", AttackSpeedBonus));
        //     Infos.Add(new ItemInfo("AttackRangeBonus", AttackRangeBonus));
        //     Infos.Add(new ItemInfo("ShotSpeedBonus", ShotSpeedBonus));  
        // }
    }
}