using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.StateMachineLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Simple;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic
{
    public class ZombieShootingSystem : MonoBehaviour, IShootingSystem
    {
        private IStateMachine<ShootingState> _stateMachine;

        private IPlayerGameplayInputHandler _gameplayInputHandler;
        private IEquipment _equipment;
        private IEquipmentContainer _equipmentContainer;
        private IEnemiesDetector _enemiesDetector;
        private IPlayerAnimator _playerAnimator;
        private IAim _aim;
        
        private ShootingSystemConfig _shootingSystemConfig;

        [SerializeField] private Weapon _firstWeapon;
        [SerializeField] private Weapon _secondWeapon;
        [SerializeField] private GrenadeLauncher _grenadeLauncher;

        public void Initialize()
        {
            _gameplayInputHandler = new PlayerGameplayInputHandler();
            _equipment = new Equipment();
            
            _playerAnimator = GetComponentInChildren<IPlayerAnimator>();
            _equipmentContainer = GetComponent<IEquipmentContainer>();
            _aim = GetComponentInChildren<IAim>();

            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            
            _enemiesDetector = new EnemiesDetector(transform, _equipment);
            
            _stateMachine = new SimpleStateMachine<ShootingState>();
            _stateMachine.Add(ShootingState.Initializing, new Initializing(ShootingState.Initializing, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Searching, new Searching(ShootingState.Searching, _stateMachine, _gameplayInputHandler,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Shooting, new Shooting(ShootingState.Shooting, _stateMachine,_gameplayInputHandler,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Reloading, new Reloading(ShootingState.Reloading, _stateMachine,_gameplayInputHandler,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Switching, new Switching(ShootingState.Switching, _stateMachine,_gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.GrenadeLaunching, new GrenadeLaunching(ShootingState.GrenadeLaunching, _stateMachine,_gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer, 
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Stopping, new Stopping(ShootingState.Switching, _stateMachine, _gameplayInputHandler,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            
            _aim.Initialize(_enemiesDetector,_equipment);
            _equipment.Initialize(_firstWeapon,_secondWeapon,_grenadeLauncher);
        }

        public void Prepare()
        {
            _stateMachine.Start(ShootingState.Initializing);
            _enemiesDetector.Start();
        }

        public void Stop()
        {
            _stateMachine.TransitToState(ShootingState.Stopping);
            _enemiesDetector.Stop();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}