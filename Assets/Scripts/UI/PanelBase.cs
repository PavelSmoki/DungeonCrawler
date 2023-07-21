using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _nameLabel;
        [SerializeField] protected TextMeshProUGUI _rarenessLabel;
        [SerializeField] protected TextMeshProUGUI _damageLabel;
        [SerializeField] protected TextMeshProUGUI _attackSpeedLabel;
        [SerializeField] protected TextMeshProUGUI _critChanceLabel;
        [SerializeField] protected TextMeshProUGUI _critModifierLabel;
        [SerializeField] protected TextMeshProUGUI _attackRangeLabel;

        protected void SetupLabels(string name, Rareness rareness, float damage, float attackSpeed,
            float critChance, float critModifier, float attackRange)
        {
            _nameLabel.text = name.ToUpper();
            _rarenessLabel.text = rareness.ToString().ToUpper();
            var color = rareness switch
            {
                Rareness.Uncommon => new Color(0.2f, 0.79f, 1f),
                Rareness.Rare => Color.blue,
                Rareness.Mythic => new Color(0.6f, 0f, 0.6f),
                Rareness.Legendary => new Color(1f, 0.502f, 0f),
                _ => Color.white
            };
            _rarenessLabel.color = color;
            _damageLabel.text = $"Damage: {damage}";
            _attackSpeedLabel.text = $"Attack Speed: {attackSpeed}";
            _critChanceLabel.text = $"Crit Chance: {critChance * 100}%";
            _critModifierLabel.text = $"Crit Modifier: {critModifier}x ";
            _attackRangeLabel.text = $"Attack Range: {attackRange}";
        }
    }
}