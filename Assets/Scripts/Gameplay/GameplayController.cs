using Cysharp.Threading.Tasks;
using Game.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GameplayController : IInitializable
    {
        private readonly EnemyFactory _enemyFactory;

        public GameplayController(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public async void Initialize()
        {
            // var room = 1;
            // var roomPrefab = await Addressables.LoadAssetAsync<GameObject>("Room_" + room).ToUniTask();
            var roomPrefab = await Addressables.LoadAssetAsync<GameObject>("DebugRoom").ToUniTask();
            var roomObj = Object.Instantiate(roomPrefab);
            var roomData = roomObj.GetComponent<RoomData>();
            _enemyFactory.Create(roomData);
        }
    }
}
