using Game.Items.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Items
{
    public static class ItemGenerator
    {
        public static WeaponBase GenerateWeapon()
        {
            var item = Addressables.LoadAssetAsync<GameObject>("Weapon" + Random.Range(1, 3)).WaitForCompletion()
                .GetComponent<WeaponBase>();
            return item;
        }

        public static Armor.Armor GenerateArmor()
        {
            var item = Addressables.LoadAssetAsync<GameObject>("Armor" + 1).WaitForCompletion()
                .GetComponent<Armor.Armor>();
            return item;
        }
    }
}