using Game.Gameplay;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    [UsedImplicitly]
    public class EnemyFactory
    {
        private readonly DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public void Create(RoomData roomData)
        {
            foreach (var enemyInfo in roomData.EnemiesInfos)
            {
                var enemy = Object.Instantiate(enemyInfo.Prefab, enemyInfo.EnemySpawn.GetTransform()).GetComponent<EnemyBase>();
                _container.Inject(enemy);
                roomData.EnemyCount.Value++;
                enemy.SetCurrentRoomData(roomData);
            }
        }
    }
}