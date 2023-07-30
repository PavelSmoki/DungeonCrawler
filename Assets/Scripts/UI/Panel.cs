using System;
using System.Collections.Generic;
using Game.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private TextMeshProUGUI _rarenessLabel;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private InfoSegment _infoSegmentPrefab;
        [SerializeField] private SegmentTextProvider _textProvider;

        private List<InfoSegment> _infoSegments = new();

        public Action OnTakeItem;

        public void Setup(List<ItemInfo> itemInfos, string itemName, Rareness rareness, Sprite sprite)
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
            _itemImage.sprite = sprite;

            _infoSegments = new List<InfoSegment>();
            foreach (var info in itemInfos)
            {
                if (info.Stat != 0f)
                {
                    var obj = Instantiate(_infoSegmentPrefab, _contentRoot);
                    _infoSegments.Add(obj);
                    var icon = _textProvider.GetFragmentInfo(out var text, info.Id, info.Stat);
                    obj.SetInfo(text, icon);
                }
            }
        }

        public void DestroySegments()
        {
            foreach (var segment in _infoSegments)
            {
                Destroy(segment.gameObject);
            }
        }

        public void OnTakeItemButtonClick()
        {
            OnTakeItem?.Invoke();
        }
    }
}