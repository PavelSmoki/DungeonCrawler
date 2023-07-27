using Game.Items.Weapons;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MeleeWeaponPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _damageLabel;
        [SerializeField] private TextMeshProUGUI _attackSpeedLabel;
        [SerializeField] private TextMeshProUGUI _critChanceLabel;
        [SerializeField] private TextMeshProUGUI _critModifierLabel;
        [SerializeField] private TextMeshProUGUI _attackRangeLabel;

        public void Setup(string itemName, Rareness rareness, float damage, float attackSpeed, float critChance,
            float critModifier, float attackRange)
        {
            BaseSetup(itemName, rareness);
            _damageLabel.text = $"Damage: {damage}";
            _attackSpeedLabel.text = $"Attack Speed: {attackSpeed}";
            _critChanceLabel.text = $"Crit Chance: {critChance * 100}%";
            _critModifierLabel.text = $"Crit Modifier: {critModifier}x ";
            _attackRangeLabel.text = $"Attack Range: {attackRange}";
        }
    }
}