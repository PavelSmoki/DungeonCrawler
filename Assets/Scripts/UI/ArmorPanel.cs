using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ArmorPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _moveSpeedModifierLabel;
        [SerializeField] private TextMeshProUGUI _damageModifierLabel;
        [SerializeField] private TextMeshProUGUI _attackSpeedModifierLabel;
        [SerializeField] private TextMeshProUGUI _critChanceModifierLabel;
        [SerializeField] private TextMeshProUGUI _attackRangeModifierLabel;
        [SerializeField] private TextMeshProUGUI _shotSpeedModifierLabel;
        public void Setup(string itemName, Rareness rareness, float moveSpeedModifier, float damageModifier,
            float attackSpeedModifier, float critChanceModifier, float attackRangeModifier, float shotSpeedModifier)
        {
            BaseSetup(itemName, rareness);
            _moveSpeedModifierLabel.text = $"Speed + {moveSpeedModifier * 100}%";
            _damageModifierLabel.text = $"Damage + {damageModifier * 100}%";
            _attackSpeedModifierLabel.text = $"Attack Speed + {attackSpeedModifier * 100}%";
            _critChanceModifierLabel.text = $"Crit Chance + {critChanceModifier * 100}%";
            _attackRangeModifierLabel.text = $"Attack Range + {attackRangeModifier * 100}%";
            _shotSpeedModifierLabel.text = $"Shot Speed + {shotSpeedModifier * 100}%";
        }
        
    }
}