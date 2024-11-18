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
        [SerializeField] private AudioSource _openChestAudioSource;
        [SerializeField] private AudioSource _closeChestAudioSource;
        [SerializeField] private AudioSource _takeItemAudioSource;

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
            _panel.OnTakeItem += TakeItem;
            SpawnItem();
            _item.gameObject.SetActive(false);
        }

        private void SpawnItem()
        {
            if (Random.Range(0, 2) == 0)
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                if (_item != null)
                {
                    if(!_openChestAudioSource.isPlaying && !_panel.gameObject.activeSelf) 
                    {
                        _openChestAudioSource.Play();
                    }
                    
                    _item.gameObject.SetActive(true);
                    PanelSetup(_item);
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
                    
                    if (!_closeChestAudioSource.isPlaying)
                    {
                        _closeChestAudioSource.Play();
                    }
                }   
            }
        }

        private void PanelSetup(Item item)
        {
            _panel.DestroySegments();
            _panel.Setup(item.Infos, item.Name, item.Rareness,
                item.GetComponentInChildren<SpriteRenderer>().sprite);
            _panel.gameObject.SetActive(true);
        }

        private void TakeItem()
        {
            if (_isWeapon)
            {
                var item = _player.TakeItem((WeaponBase)_item);
                if (item == null)
                {
                    _panel.gameObject.SetActive(false);
                    _closeChestAudioSource.Play();
                }
                else
                {
                    item.transform.SetParent(transform);
                    item.transform.position = transform.position + _itemSpawnOffset;
                    PanelSetup(item);
                }
                
                _item = item;
            }
            else
            {
                var item = _player.TakeItem((Armor.Armor)_item);
                if (item == null)
                {
                    _panel.gameObject.SetActive(false);
                    _closeChestAudioSource.Play();
                }
                else
                {
                    item.SpriteRenderer.enabled = true;
                    item.transform.SetParent(transform);
                    item.transform.position = transform.position + _itemSpawnOffset;
                    PanelSetup(item);
                }
                
                _item = item;
            }

            if (!_takeItemAudioSource.isPlaying)
            {
                _takeItemAudioSource.Play();
            }
        }
    }
}