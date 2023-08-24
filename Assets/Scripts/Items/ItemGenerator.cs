using Game.Gameplay;
using Game.Items.Weapons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Items
{
    [UsedImplicitly]
    public class ItemGenerator
    {
        private static AssetProvider _assetProvider;

        public ItemGenerator(AssetProvider assetProvider) => _assetProvider = assetProvider;

        public static WeaponBase GenerateWeapon()
        {
            var assetReference = _assetProvider.Weapons[Random.Range(1, _assetProvider.Weapons.Count)];
            var item = Addressables.LoadAssetAsync<GameObject>(assetReference).WaitForCompletion()
                .GetComponent<WeaponBase>();
            return item;
        }

        public static Armor.Armor GenerateArmor()
        {
            var assetReference = _assetProvider.Armors[Random.Range(1, _assetProvider.Armors.Count)];
            var item = Addressables.LoadAssetAsync<GameObject>(assetReference).WaitForCompletion()
                .GetComponent<Armor.Armor>();
            return item;
        }
    }
}