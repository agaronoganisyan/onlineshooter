using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.StateMachineLogic;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Simple;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic
{
    public class PlayerShootingSystem : MonoBehaviour, IShootingSystem
    {
        private IStateMachine<ShootingState> _stateMachine;

        private Unit _unit;
        private IPlayerGameplayInputHandler _gameplayInputHandler;
        private IEnemiesDetector _enemiesDetector;
        private IEquipment _equipment;
        private IEquipmentContainer _equipmentContainer;
        private IPlayerAnimator _playerAnimator;
        private IAimSystem _aimSystem;
        private IAim _aim;
        
        private ShootingSystemConfig _shootingSystemConfig;

        public void Initialize()
        {
            _gameplayInputHandler = ServiceLocator.Get<IPlayerGameplayInputHandler>();
            _equipment = ServiceLocator.Get<IEquipment>();
            _aimSystem = ServiceLocator.Get<IAimSystem>();

            _unit = GetComponent<Unit>();
            _playerAnimator = GetComponentInChildren<IPlayerAnimator>();
            _equipmentContainer = GetComponent<IEquipmentContainer>();
            _aim = GetComponentInChildren<IAim>();

            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            
            _enemiesDetector = new EnemiesDetector(transform, _equipment);
            
            _stateMachine = new SimpleStateMachine<ShootingState>();
            _stateMachine.Add(ShootingState.Initializing, new Initializing(ShootingState.Initializing, _stateMachine, _gameplayInputHandler, _unit, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Searching, new Searching(ShootingState.Searching, _stateMachine, _gameplayInputHandler, _unit,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Shooting, new Shooting(ShootingState.Shooting, _stateMachine,_gameplayInputHandler, _unit,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Reloading, new Reloading(ShootingState.Reloading, _stateMachine,_gameplayInputHandler, _unit,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Switching, new Switching(ShootingState.Switching, _stateMachine,_gameplayInputHandler, _unit, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.GrenadeLaunching, new GrenadeLaunching(ShootingState.GrenadeLaunching, _stateMachine, _gameplayInputHandler, _unit, _playerAnimator, _equipment, _equipmentContainer, 
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Stopping, new Stopping(ShootingState.Switching, _stateMachine, _gameplayInputHandler, _unit, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            
            _stateMachine.Initialize(ShootingState.Initializing);
            _aim.Initialize(_enemiesDetector,_equipment);
            _aimSystem.Initialize(_aim);
        }

        public void Prepare()
        {
            _enemiesDetector.Start(); 
            _equipment.Reset();
            _stateMachine.TransitToState(ShootingState.Searching);
        }

        public void Stop()
        {
            _stateMachine.TransitToState(ShootingState.Stopping);
            _enemiesDetector.Stop();
        }

        public void FixedTick()
        {
            _stateMachine.Tick();
        }
    }
}