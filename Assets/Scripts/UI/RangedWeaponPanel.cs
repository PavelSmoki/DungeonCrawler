using Game.Items.Weapons;
using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class RangedWeaponPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _damageLabel;
        [SerializeField] private TextMeshProUGUI _attackSpeedLabel;
        [SerializeField] private TextMeshProUGUI _critChanceLabel;
        [SerializeField] private TextMeshProUGUI _critModifierLabel;
        [SerializeField] private TextMeshProUGUI _attackRangeLabel;
        [SerializeField] private TextMeshProUGUI _shotSpeedLabel;

        public void Setup(string itemName, Rareness rareness, float damage, float attackSpeed, float critChance,
            float critModifier, float attackRange, float shotSpeed)
        {
            BaseSetup(itemName, rareness);
            _damageLabel.text = $"Damage: {damage}";
            _attackSpeedLabel.text = $"Attack Speed: {attackSpeed}";
            _critChanceLabel.text = $"Crit Chance: {critChance * 100}%";
            _critModifierLabel.text = $"Crit Modifier: {critModifier}x ";
            _attackRangeLabel.text = $"Attack Range: {attackRange}";
            _shotSpeedLabel.text = $"Shot Speed: {shotSpeed}";
        }
    }
}