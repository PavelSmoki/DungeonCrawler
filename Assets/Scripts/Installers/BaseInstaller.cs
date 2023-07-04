using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class BaseInstaller : MonoInstaller
    { 
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private FixedJoystick _fixedJoystick;
        [SerializeField] private List<EnemyData> _enemiesData;

        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<FixedJoystick>().FromInstance(_fixedJoystick);
            Container.Bind<EnemyFactory>().AsSingle();
            
            
            InstallScriptableObjects();
        }

        private void InstallScriptableObjects()
        {
            foreach (var enemyData in _enemiesData)
            {
                Container.Bind<EnemyData>().FromInstance(enemyData);
            }
            
        }
    }
}