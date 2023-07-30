using Game.Items.Weapons;
using Game.Player;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Items
{
    public class ItemPedestal : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [SerializeField] private Panel _panel;

        private Item _item;

        private readonly Vector3 _itemSpawnOffset = new(0, 0.65f, 0);
        private bool _isWeapon;

        private IPlayer _player;

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            InitPanelAction();
            SpawnItem();
            _item.gameObject.SetActive(false);
        }

        private void SpawnItem()
        {
            if (Random.Range(0, 5) == 0)
            {
                _item = Instantiate(ItemGenerator.GenerateWeapon(), transform.position + _itemSpawnOffset,
                    Quaternion.identity);
                _isWeapon = true;
            }
            else
            {
                _item = Instantiate(ItemGenerator.GenerateArmor(), transform.position + _itemSpawnOffset,
                    Quaternion.identity);
                _isWeapon = false;
            }
        }

        private void InitPanelAction()
        {
            _panel.OnTakeItem += TakeItem;
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
                if (_item != null)
                {
                    _item.gameObject.SetActive(false);
                    _panel.gameObject.SetActive(false);
                }
            }
        }

        private void PanelSetup()
        {
            _panel.DestroySegments();
            _panel.Setup(_item.Infos, _item.Name, _item.Rareness,
                _item.GetComponentInChildren<SpriteRenderer>().sprite);
            _panel.gameObject.SetActive(true);
        }

        private void TakeItem()
        {
            if (_isWeapon)
            {
                var item = _player.TakeItem((WeaponBase)_item, transform);
                if (item == null)
                {
                    _panel.gameObject.SetActive(false);
                }
                else
                {
                    _item = item;
                    _item.transform.position = transform.position + _itemSpawnOffset;
                    PanelSetup();
                }
            }
            else
            {
                var item = _player.TakeItem((Armor.Armor)_item, transform);
                if (item == null)
                {
                    _panel.gameObject.SetActive(false);
                }
                else
                {
                    _item = item;
                    _item.transform.position = transform.position + _itemSpawnOffset;
                    PanelSetup();
                }
            }
        }
    }
}