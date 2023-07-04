using UnityEngine;
using Zenject;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        private Player _player;
        private EnemyData _enemyData;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        public void SetEnemyData(EnemyData enemyData)
        {
            _enemyData = enemyData;
        }
    }
}