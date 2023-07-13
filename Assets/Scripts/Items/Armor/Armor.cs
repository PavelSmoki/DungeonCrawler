using Game.Items.Weapons;
using UnityEngine;

namespace Game.Items.Armor
{
    public class Armor : MonoBehaviour
    {
        [field: SerializeField] protected Rareness Rareness { get; private set; }
        [field: SerializeField] protected ArmorSlot ArmorSlot { get; private set; }
        [field: SerializeField] protected float MoveSpeedBonus { get; private set; }
        [field: SerializeField] protected float AttackSpeedBonus { get; private set; }
        [field: SerializeField] protected float AttackRangeBonus { get; private set; }
        [field: SerializeField] protected float ShotSpeedBonus { get; private set; }
        
    }
}