using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.StateMachineLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Simple;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic
{
    public class ShootingSystem : MonoBehaviour, IShootingSystem
    {
        private IStateMachine<ShootingState> _stateMachine;
        
        private IEquipment _equipment;
        private IEquipmentContainer _equipmentContainer;
        private IEnemiesDetector _enemiesDetector;
        [SerializeField] private HeroAnimator _heroAnimator;
        
        [SerializeField] private ShootingSystemConfig _shootingSystemConfig;
        [SerializeField] private GrenadeLaunchingConfig _grenadeLaunchingConfig;

        [SerializeField] private Transform _rightHandContainer;
        [SerializeField] private Transform _leftHandContainer;
        [SerializeField] private Transform _firstWeaponContainer;
        [SerializeField] private Transform _secondWeaponContainer;
        [SerializeField] private Transform _crosshair;
        [SerializeField] private Transform _crosshairBasePosition;
        
        [SerializeField] private float _crosshairMovementSpeed;
        
        private void Awake()
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            
            _equipmentContainer = new EquipmentContainer();
            _equipmentContainer.SetUp(_rightHandContainer,_leftHandContainer,_firstWeaponContainer, _secondWeaponContainer);
            
            _enemiesDetector = new EnemiesDetector(_shootingSystemConfig, _equipment.CurrentWeapon.WeaponConfig, transform);
            
            _stateMachine = new SimpleStateMachine<ShootingState>();
            _stateMachine.Add(ShootingState.Initializing, new Initializing(ShootingState.Initializing, _stateMachine, _heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
            _stateMachine.Add(ShootingState.Searching, new Searching(ShootingState.Searching, _stateMachine, _heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
            _stateMachine.Add(ShootingState.Shooting, new Shooting(ShootingState.Shooting, _stateMachine,_heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
            _stateMachine.Add(ShootingState.Reloading, new Reloading(ShootingState.Reloading, _stateMachine,_heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
            _stateMachine.Add(ShootingState.Switching, new Switching(ShootingState.Switching, _stateMachine, _heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
            _stateMachine.Add(ShootingState.GrenadeLaunching, new GrenadeLaunching(ShootingState.GrenadeLaunching, _stateMachine, _heroAnimator, _equipment, _equipmentContainer,
                _enemiesDetector, _shootingSystemConfig, _grenadeLaunchingConfig,_crosshair, _crosshairBasePosition, _crosshairMovementSpeed));
        } 
        
        private void Start()
        {
            _stateMachine.Start(ShootingState.Initializing);
            _enemiesDetector.Start();
            
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}