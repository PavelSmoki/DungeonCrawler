using Game.Enemies;
using Game.Gameplay;
using Game.Player;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private MeleeWeaponPanel _meleeWeaponPanel;
        [SerializeField] private RangedWeaponPanel _rangeWeaponPanel;
        

        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<GameUI>().FromInstance(_gameUI);
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<MeleeWeaponPanel>().FromInstance(_meleeWeaponPanel);
            Container.Bind<RangedWeaponPanel>().FromInstance(_rangeWeaponPanel);
        }
    }
}