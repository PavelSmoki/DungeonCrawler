using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InfoSegment : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _textField;

        public void SetInfo(string text, Sprite sprite)
        {
            _icon.sprite = sprite;
            _textField.text = text;
        }
    }
}