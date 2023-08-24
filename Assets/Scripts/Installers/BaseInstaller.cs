using Game.Enemies;
using Game.Gameplay;
using Game.Items;
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
        [SerializeField] private AssetProvider _assetProvider;

        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<ItemGenerator>().AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInstance(_assetProvider);
            Container.BindInstance(_gameUI);
            Container.BindInstance(_grid);
            Container.BindInstance(_damageUI);
            Container.BindInstance(_gameOverUI);
        }
    }
}