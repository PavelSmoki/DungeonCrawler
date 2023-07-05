using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game
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
            for (int i = 0; i < roomData.EnemiesData.Count; i++)
            {
                var enemy = Object.Instantiate(roomData.EnemiesData[i].Prefab, roomData.EnemiesPos[i].GetTransform())
                    .GetComponent<AEnemy>();
                enemy.SetEnemyData(roomData.EnemiesData[i]);
                _container.Inject(enemy);
            }
        }
    }
}