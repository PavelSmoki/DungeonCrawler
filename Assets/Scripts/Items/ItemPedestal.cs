using Game.Items.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ItemPedestal : MonoBehaviour
    {
        [SerializeField] private MeleeWeaponPanel _meleeWeaponPanel;
        [SerializeField] private RangedWeaponPanel _rangedWeaponPanel;

        [SerializeField] private GameObject _itemPrefab;

        private readonly Vector3 _itemSpawnOffset = new(0, 0.65f, 0);
        private GameObject _item;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_item == null)
                {
                    _item = Instantiate(_itemPrefab, transform.position + _itemSpawnOffset, Quaternion.identity);
                    PanelSetup();
                }
            }
        }

        private void PanelSetup()
        {
            if (_item.TryGetComponent<MeleeWeapon>(out var itemStats))
            {
                _meleeWeaponPanel.gameObject.SetActive(true);
                _meleeWeaponPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    _item.GetComponentInChildren<SpriteRenderer>().sprite;
                _meleeWeaponPanel.SetupLabels(
                    itemStats.Name, itemStats.Rareness, itemStats.Damage, itemStats.AttackSpeed,
                    itemStats.CritChance, itemStats.CritModifier, itemStats.AttackRange);
            }
            else if (_item.TryGetComponent<RangedWeapon>(out var itemStats1))
            {
                _rangedWeaponPanel.gameObject.SetActive(true);
                _rangedWeaponPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    _item.GetComponentInChildren<SpriteRenderer>().sprite;
                _rangedWeaponPanel.SetupLabels(
                    itemStats1.Name, itemStats1.Rareness, itemStats1.Damage, itemStats1.AttackSpeed,
                    itemStats1.CritChance, itemStats1.CritModifier, itemStats1.AttackRange,
                    itemStats1.ShotSpeed);
            }
            else
            {
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(_item);
                _meleeWeaponPanel.gameObject.SetActive(false);
                _rangedWeaponPanel.gameObject.SetActive(false);
            }
        }
    }
}