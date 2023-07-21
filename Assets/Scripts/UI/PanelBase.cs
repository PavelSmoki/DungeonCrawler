using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI NameLabel;
        [SerializeField] protected TextMeshProUGUI RarenessLabel;
        [SerializeField] protected TextMeshProUGUI DamageLabel;
        [SerializeField] protected TextMeshProUGUI AttackSpeedLabel;
        [SerializeField] protected TextMeshProUGUI CritChanceLabel;
        [SerializeField] protected TextMeshProUGUI CritModifierLabel;
        [SerializeField] protected TextMeshProUGUI AttackRangeLabel;

        public void SetupLabels(string itemName, Rareness rareness, float damage, float attackSpeed,
            float critChance, float critModifier, float attackRange)
        {
            NameLabel.text = itemName.ToUpper();
            RarenessLabel.text = rareness.ToString().ToUpper();
            var color = rareness switch
            {
                Rareness.Uncommon => new Color(0.2f, 0.79f, 1f),
                Rareness.Rare => Color.blue,
                Rareness.Mythic => new Color(0.6f, 0f, 0.6f),
                Rareness.Legendary => new Color(1f, 0.502f, 0f),
                _ => Color.white
            };
            RarenessLabel.color = color;
            DamageLabel.text = $"Damage: {damage}";
            AttackSpeedLabel.text = $"Attack Speed: {attackSpeed}";
            CritChanceLabel.text = $"Crit Chance: {critChance * 100}%";
            CritModifierLabel.text = $"Crit Modifier: {critModifier}x ";
            AttackRangeLabel.text = $"Attack Range: {attackRange}";
        }
    }
}