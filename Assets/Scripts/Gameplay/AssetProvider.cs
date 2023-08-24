using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Tools/AssetProvider", fileName = "AssetProvider")]
    public class AssetProvider : ScriptableObject
    {
        [field: SerializeField] public List<AssetReference> Rooms { get; private set; }
        [field: SerializeField] public List<AssetReference> Weapons { get; private set; }
        [field: SerializeField] public List<AssetReference> Armors { get; private set; }
    }
}