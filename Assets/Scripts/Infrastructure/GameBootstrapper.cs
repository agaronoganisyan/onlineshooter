using ConfigsLogic;
using Gameplay.CameraLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] GrenadeLauncher _grenadeLauncher;
        
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerInfoBlockConfig _playerInfoBlockConfig;
        [SerializeField] private HealthSystemConfig _healthSystemConfig;
        
        [SerializeField] private Player _player;
        [SerializeField] private CameraController _cameraController;

        [SerializeField] private SharedGameplayCanvas _sharedGameplayCanvas;
        
        private void Awake()
        {
            RegisterServices();
            InitServices();
        }

        private void RegisterServices()
        {
            IInputService inputService = new InputService();
            ServiceLocator.Register<IInputService>(inputService);
            
            ServiceLocator.Register<IEquipment>(new Equipment(inputService));
            
            ServiceLocator.Register<ICameraController>(_cameraController);
            
            ServiceLocator.Register<ISharedGameplayCanvasObjectFactory>(
                new SharedGameplayCanvasObjectFactory(_sharedGameplayCanvas, _playerConfig,
                    _playerInfoBlockConfig, _cameraController.Camera));
            
            ServiceLocator.Register<Player>(_player);
            
            ServiceLocator.Register<HealthSystemConfig>(_healthSystemConfig);
        }

        private void InitServices()
        {
            IInputService inputService = ServiceLocator.Get<IInputService>();
            inputService.Initialize();

            IEquipment equipment = ServiceLocator.Get<IEquipment>();
            equipment.SetUp(_weapons,_grenadeLauncher);
            
            _sharedGameplayCanvas.Initialize();
            _sharedGameplayCanvas.StartUpdating();

        }
    }
}