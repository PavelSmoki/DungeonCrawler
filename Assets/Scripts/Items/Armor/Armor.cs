using System;
using Game.Items.Weapons;
using UnityEngine;

namespace Game.Items.Armor
{
    [Serializable]
    public class Armor : MonoBehaviour
    {
        [field: SerializeField] public Rareness Rareness { get; private set; }
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public float MoveSpeedBonus { get; private set; }
        [field: SerializeField] public float DamageBonus { get; private set; }
        [field: SerializeField] public float CritChanceBonus { get; private set; }
        [field: SerializeField] public float AttackSpeedBonus { get; private set; }
        [field: SerializeField] public float AttackRangeBonus { get; private set; }
        [field: SerializeField] public float ShotSpeedBonus { get; private set; }
        
    }
}