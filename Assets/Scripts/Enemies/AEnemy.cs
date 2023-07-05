using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class AEnemy : MonoBehaviour
    {
        protected Player _player;
        protected EnemyData _enemyData;
        protected bool _isDelayed { get; private set; }

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        public void SetEnemyData(EnemyData enemyData)
        {
            _enemyData = enemyData;
        }

        private async UniTaskVoid BehaviorDelayAfterSpawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _isDelayed = true;
        }

        private void Start()
        {
            BehaviorDelayAfterSpawn();
        }
    }
}