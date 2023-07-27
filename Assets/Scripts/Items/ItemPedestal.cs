using Game.Items.Weapons;
using Game.Player;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Items
{
    public class ItemPedestal : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [SerializeField] private MeleeWeaponPanel _meleeWeaponPanel;
        [SerializeField] private RangedWeaponPanel _rangedWeaponPanel;
        [SerializeField] private ArmorPanel _armorPanel;
        [SerializeField] private GameObject _itemPrefab;

        private readonly Vector3 _itemSpawnOffset = new(0, 0.65f, 0);
        private GameObject _item;
        private IPlayer _player;

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            InitPanelAction(_meleeWeaponPanel);
            InitPanelAction(_rangedWeaponPanel);
            InitPanelAction(_armorPanel);
            
            _item = Instantiate(_itemPrefab, transform.position + _itemSpawnOffset, Quaternion.identity);
            _item.gameObject.SetActive(false);
        }

        private void InitPanelAction(PanelBase panel)
        { 
            panel.OnTakeItem += TakeItem;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                if (_item != null)
                {
                    _item.gameObject.SetActive(true);
                    PanelSetup();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                _item.gameObject.SetActive(false);
                _meleeWeaponPanel.gameObject.SetActive(false);
                _rangedWeaponPanel.gameObject.SetActive(false);
            }
        }

        private void PanelSetup()
        {
            if (_item.TryGetComponent<MeleeWeapon>(out var itemStats))
            {
                _meleeWeaponPanel.gameObject.SetActive(true);
                SetItemSprite(_meleeWeaponPanel);
                _meleeWeaponPanel.Setup(itemStats.Name, itemStats.Rareness, itemStats.Damage, itemStats.AttackSpeed,
                    itemStats.CritChance, itemStats.CritModifier, itemStats.AttackRange);
            }
            else if (_item.TryGetComponent<RangedWeapon>(out var itemStats1))
            {
                _rangedWeaponPanel.gameObject.SetActive(true);
                SetItemSprite(_rangedWeaponPanel);
                _rangedWeaponPanel.Setup(itemStats1.Name, itemStats1.Rareness, itemStats1.Damage, itemStats1.AttackSpeed,
                    itemStats1.CritChance, itemStats1.CritModifier, itemStats1.AttackRange,
                    itemStats1.ShotSpeed);
            }
            else if (_item.TryGetComponent<Armor.Armor>(out var itemStats2))
            {
                _armorPanel.gameObject.SetActive(true);
                SetItemSprite(_armorPanel);
                _armorPanel.Setup(itemStats2.Name, itemStats2.Rareness, itemStats2.MoveSpeedBonus,
                    itemStats2.DamageBonus, itemStats2.AttackSpeedBonus, itemStats2.CritChanceBonus,
                    itemStats2.AttackRangeBonus, itemStats2.ShotSpeedBonus);
            }
        }

        private void SetItemSprite(PanelBase panel)
        {
            panel.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                _item.GetComponentInChildren<SpriteRenderer>().sprite;
        }

        private void TakeItem()
        {
           
        }
    }
}