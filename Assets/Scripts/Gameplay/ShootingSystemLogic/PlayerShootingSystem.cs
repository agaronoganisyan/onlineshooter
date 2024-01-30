using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.ShootingSystemLogic.StateMachineLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Simple;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.ShootingSystemLogic
{
    public class PlayerShootingSystem : MonoBehaviour, IShootingSystem
    {
        private IStateMachine<ShootingState> _stateMachine;
        
        private IEquipment _equipment;
        private IEquipmentContainer _equipmentContainer;
        private IEnemiesDetector _enemiesDetector;
        private IPlayerAnimator _playerAnimator;
        private IAimSystem _aimSystem;
        private IAim _aim;
        
        [SerializeField] private ShootingSystemConfig _shootingSystemConfig;
        [SerializeField] private GrenadeLaunchingConfig _grenadeLaunchingConfig;

        [SerializeField] private Transform _rightHandContainer;
        [SerializeField] private Transform _leftHandContainer;
        [SerializeField] private Transform _firstWeaponContainer;
        [SerializeField] private Transform _secondWeaponContainer;
        
        public void Initialize()
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            _aimSystem = ServiceLocator.Get<IAimSystem>();
            
            _playerAnimator = GetComponentInChildren<IPlayerAnimator>();
            _equipmentContainer = GetComponent<IEquipmentContainer>();
            _aim = GetComponentInChildren<IAim>();
            
            _equipmentContainer = new EquipmentContainer();
            _equipmentContainer.SetUp(_rightHandContainer,_leftHandContainer,_firstWeaponContainer, _secondWeaponContainer);
            
            _enemiesDetector = new EnemiesDetector(_shootingSystemConfig, transform);
            
            _stateMachine = new SimpleStateMachine<ShootingState>();
            _stateMachine.Add(ShootingState.Initializing, new Initializing(ShootingState.Initializing, _stateMachine, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Searching, new Searching(ShootingState.Searching, _stateMachine, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Shooting, new Shooting(ShootingState.Shooting, _stateMachine,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Reloading, new Reloading(ShootingState.Reloading, _stateMachine,_playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.Switching, new Switching(ShootingState.Switching, _stateMachine, _playerAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _aim, _shootingSystemConfig));
            _stateMachine.Add(ShootingState.GrenadeLaunching, new GrenadeLaunching(ShootingState.GrenadeLaunching, _stateMachine, _playerAnimator, _equipment, _equipmentContainer, 
                _enemiesDetector, _aim, _shootingSystemConfig, _grenadeLaunchingConfig));
            _stateMachine.Add(ShootingState.Stopping, new Stopping(ShootingState.Switching, _stateMachine, _playerAnimator, _equipment, _equipmentContainer,
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