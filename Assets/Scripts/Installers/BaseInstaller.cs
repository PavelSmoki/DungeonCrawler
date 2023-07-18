using System.Collections.Generic;
using Game.Enemies;
using Game.Gameplay;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private FixedJoystick _MoveJoystick;
        [SerializeField] private FixedJoystick _AttackJoystick;
        [SerializeField] private Button _weaponSwitchButton;

        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<FixedJoystick>().WithId("MoveJoystick").FromInstance(_MoveJoystick);
            Container.Bind<FixedJoystick>().WithId("AttackJoystick").FromInstance(_AttackJoystick);
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<Button>().FromInstance(_weaponSwitchButton);

        }
    }
}