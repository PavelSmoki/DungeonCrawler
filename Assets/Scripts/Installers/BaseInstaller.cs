using Game.Enemies;
using Game.Enemies.EnemiesInstances;
using Game.Gameplay;
using Game.Player;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Grid _grid;
        [SerializeField] private DamageUI _damageUI;
        [SerializeField] private GameOverUI _gameOverUI;

        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInstance(_gameUI);
            Container.BindInstance(_grid);
            Container.BindInstance(_damageUI);
            Container.BindInstance(_gameOverUI);
        }
    }
}