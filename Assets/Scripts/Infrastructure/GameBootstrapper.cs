using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.GameFactoryLogic;
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
        
        private async void Awake()
        {
            RegisterServices();
            await InitServices();
        }

        private void RegisterServices()
        {
            IInputService inputService = new InputService();
            ServiceLocator.Register<IInputService>(inputService);
            
            ServiceLocator.Register<IEquipment>(new Equipment(inputService));
            
            ServiceLocator.Register<ICameraController>(_cameraController);
            
            ServiceLocator.Register<ISharedGameplayCanvasObjectFactory>(
                new SharedGameplayCanvasObjectFactory());
            
            ServiceLocator.Register<Player>(_player);
            
            ServiceLocator.Register<HealthSystemConfig>(_healthSystemConfig);
            
            ServiceLocator.Register<IAssetsProvider>(new AssetsProvider());
            ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());
        }

        private async UniTask InitServices()
        {
            IEquipment equipment = ServiceLocator.Get<IEquipment>();
            equipment.SetUp(_weapons,_grenadeLauncher);
            
            _player.Initialize();
            _player.Prepare();
            
            ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateAndRegisterInfrastructure();
            
            IInputService inputService = ServiceLocator.Get<IInputService>();
            inputService.Initialize();
            
            ServiceLocator.Get<IAssetsProvider>().Initialize();
        }
    }
}