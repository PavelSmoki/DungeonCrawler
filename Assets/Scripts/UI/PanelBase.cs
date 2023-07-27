using System;
using Game.Items.Weapons;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private TextMeshProUGUI _rarenessLabel;

        public Action OnTakeItem;

        protected void BaseSetup(string itemName, Rareness rareness)
        {
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
        }

        public void OnTakeItemButtonClick()
        {
            OnTakeItem?.Invoke();
        }
    }
}