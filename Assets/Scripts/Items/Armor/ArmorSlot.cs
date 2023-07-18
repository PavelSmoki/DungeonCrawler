using UnityEngine;

namespace Game.Items.Armor
{
    public class ArmorSlot
    {
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public Armor Armor { get; private set; }
    }
}