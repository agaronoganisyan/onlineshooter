using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.StateMachineLogic;
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
        
        private IPlayerGameplayInputHandler _gameplayInputHandler;
        private IEquipment _equipment;
        private IEquipmentContainer _equipmentContainer;
        private IEnemiesDetector _enemiesDetector;
        private IPlayerAnimator _playerAnimator;
        private IAimSystem _aimSystem;
        private IAim _aim;
        
        ShootingSystemConfig _shootingSystemConfig;
        
        public void Initialize()
        {
            _gameplayInputHandler = ServiceLocator.Get<IPlayerGameplayInputHandler>();
            _equipment = ServiceLocator.Get<IEquipment>();
            _aimSystem = ServiceLocator.Get<IAimSystem>();
            
            _playerAnimator = GetComponentInChildren<IPlayerAnimator>();
            _equipmentContainer = GetComponent<IEquipmentContainer>();
            _aim = GetComponentInChildren<IAim>();
            
            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            
            _enemiesDetector = new EnemiesDetector(transform);
            
            _stateMachine = new SimpleStateMachine<ShootingState>();
            _stateMachine.Add(ShootingState.Initializing, new Initializing(ShootingState.Initializing, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Searching, new Searching(ShootingState.Searching, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Shooting, new Shooting(ShootingState.Shooting, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Reloading, new Reloading(ShootingState.Reloading, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Switching, new Switching(ShootingState.Switching, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.GrenadeLaunching, new GrenadeLaunching(ShootingState.GrenadeLaunching, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Stopping, new Stopping(ShootingState.Stopping, _stateMachine, _gameplayInputHandler, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            
            _aim.Initialize(_enemiesDetector);
            _aimSystem.Initialize(_aim);
        }

        public void Prepare()
        {
            _stateMachine.Start(ShootingState.Initializing);
            _enemiesDetector.Start();
            
        }

        public void Stop()
        {
            _stateMachine.Start(ShootingState.Stopping);
            _enemiesDetector.Stop();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}