using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MeleeWeaponPanel : PanelBase
    {
        public void SetupLabels(string name, Rareness rareness, float damage, float attackSpeed, float critChance,
            float critModifier, float attackRange)
        {
            base.SetupLabels(name, rareness, damage, attackSpeed, critChance, critModifier, attackRange);
        }
    }
}