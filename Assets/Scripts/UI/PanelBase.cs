using Game.Items.Weapons;
using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public abstract class PanelBase : MonoBehaviour
    {
        [Header("LABELS")]
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private TextMeshProUGUI _rarenessLabel;
        [SerializeField] private TextMeshProUGUI _damageLabel;
        [SerializeField] private TextMeshProUGUI _attackSpeedLabel;
        [SerializeField] private TextMeshProUGUI _critChanceLabel;
        [SerializeField] private TextMeshProUGUI _critModifierLabel;
        [SerializeField] private TextMeshProUGUI _attackRangeLabel;

        private GameObject _item;

        private IPlayer _player;

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }

        public void Setup(GameObject item, string itemName, Rareness rareness, float damage, float attackSpeed,
            float critChance, float critModifier, float attackRange)
        {
            _item = item;
            
            _nameLabel.text = itemName.ToUpper();
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

        public void TakeItem()
        {
            _player.TakeItem(_item);
        }
    }
}