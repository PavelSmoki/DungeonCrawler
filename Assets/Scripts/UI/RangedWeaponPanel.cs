using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game
{
    public class RangedWeaponPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _shotSpeedLabel;

        public void SetupLabels(string name, Rareness rareness, float damage, float attackSpeed, float critChance,
            float critModifier, float attackRange, float shotSpeed)
        {
            base.SetupLabels(name, rareness, damage, attackSpeed, critChance, critModifier, attackRange);
            _shotSpeedLabel.text = $"Shot Speed: {shotSpeed}";
        }
    }
}