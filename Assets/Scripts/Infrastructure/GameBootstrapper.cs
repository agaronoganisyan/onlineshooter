using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.EquipmentFactoryLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.GameFactoryLogic;
using Infrastructure.GameStateMachineLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerInfoBlockConfig _playerInfoBlockConfig;
        [SerializeField] private HealthSystemConfig _healthSystemConfig;
        
        private async void Awake()
        {
            RegisterServices();
            await InitServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IInputService>(new InputService());
            
            ServiceLocator.Register<IEquipmentFactory>(new EquipmentFactory());
            ServiceLocator.Register<IEquipmentSystem>(new EquipmentSystem());
            ServiceLocator.Register<IEquipment>(new Equipment());
            
            ServiceLocator.Register<ISharedGameplayCanvasObjectFactory>(
                new SharedGameplayCanvasObjectFactory());
            
            ServiceLocator.Register<HealthSystemConfig>(_healthSystemConfig);
            
            ServiceLocator.Register<IOperationSystem>(new OperationSystem());
            ServiceLocator.Register<IAssetsProvider>(new AssetsProvider());
            ServiceLocator.Register<ISceneSystem>(new SceneSystem());
            ServiceLocator.Register<IGameStateMachine>(new GameStateMachine());
            ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());
        }

        private async UniTask InitServices()
        {
            ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateAndRegisterInfrastructure();
            
            ServiceLocator.Get<IInputService>().Initialize();
            
            ServiceLocator.Get<IEquipmentFactory>().Initialize();
            ServiceLocator.Get<IEquipment>().Initialize();
            ServiceLocator.Get<IEquipmentSystem>().Initialize();
            
            ServiceLocator.Get<IOperationSystem>().Initialize();
            ServiceLocator.Get<IAssetsProvider>().Initialize();
            ServiceLocator.Get<ISceneSystem>().Initialize();
            ServiceLocator.Get<IGameStateMachine>().Initialize();
        }
    }
}