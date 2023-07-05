using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game
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
            var room = 2;
            var roomPrefab = await Addressables.LoadAssetAsync<GameObject>("Room_" + room).ToUniTask();
            var roomObj = Object.Instantiate(roomPrefab);
            var roomData = roomObj.GetComponent<RoomData>();
            _enemyFactory.Create(roomData);
        }
    }
}
