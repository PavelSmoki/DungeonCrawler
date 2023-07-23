using Game.Items.Weapons;
using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class RangedWeaponPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _shotSpeedLabel;

        public void Setup(GameObject item, string itemName, Rareness rareness, float damage, float attackSpeed, float critChance,
            float critModifier, float attackRange, float shotSpeed)
        {
            base.Setup(item, itemName, rareness, damage, attackSpeed, critChance, critModifier, attackRange);
            _shotSpeedLabel.text = $"Shot Speed: {shotSpeed}";
        }
    }
}