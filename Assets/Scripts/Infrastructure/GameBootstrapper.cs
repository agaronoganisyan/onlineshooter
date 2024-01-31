using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.PointsLogic;
using Gameplay.MatchLogic.PointsLogic.PointsRuleLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EquipmentFactoryLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.UILogic.DebriefingCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.GameFactoryLogic;
using Infrastructure.GameStateMachineLogic;
using Infrastructure.LoadingCanvasLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputCanvasLogic;
using InputLogic.InputServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using LobbyLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private ForTests _forTests;
        
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerInfoBlockConfig _playerInfoBlockConfig;
        [SerializeField] private HealthSystemConfig _healthSystemConfig;
        [SerializeField] private LoadingScreenSystemConfig _loadingScreenSystemConfig;
        [SerializeField] private ShootingSystemConfig _shootingSystemConfig;

        private async void Awake()
        {
            RegisterServices();
            await InitServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IInputService>(new InputService());
            ServiceLocator.Register<IPlayerGameplayInputHandler>(new PlayerGameplayInputHandler());

            ServiceLocator.Register<IEquipmentFactory>(new EquipmentFactory());
            ServiceLocator.Register<IEquipmentSystem>(new EquipmentSystem());
            ServiceLocator.Register<IEquipment>(new Equipment());
            ServiceLocator.Register<IAimSystem>(new AimSystem());
            
            ServiceLocator.Register<ILoadingScreenSystem>(new LoadingScreenSystem());
            ServiceLocator.Register<ILobbyCanvasSystem>(new LobbyCanvasSystem());
            ServiceLocator.Register<IInputCanvasSystem>(new InputCanvasSystem());
            ServiceLocator.Register<IGameplayInfoCanvasSystem>(new GameplayInfoCanvasSystem());
            ServiceLocator.Register<IDebriefingCanvasSystem>(new DebriefingCanvasSystem());
            ServiceLocator.Register<ISharedGameplayCanvasSystem>(new SharedGameplayCanvasSystem());
            ServiceLocator.Register<ISharedGameplayCanvasObjectFactory>(new SharedGameplayCanvasObjectFactory());

            ServiceLocator.Register<IPlayerMatchInfo>(new PlayerMatchInfo());
            ServiceLocator.Register<ISpawnSystem>(new SpawnSystem());
            ServiceLocator.Register<IRespawnSystem>(new RespawnSystem());
            ServiceLocator.Register<DeathmatchPointsRuleSystem>(new DeathmatchPointsRuleSystem());
            ServiceLocator.Register<IPointsSystem>(new PointsSystem());
            ServiceLocator.Register<IOperationSystem>(new OperationSystem());
            ServiceLocator.Register<ITeamsSystem>(new TeamsSystem());
            ServiceLocator.Register<IMatchSystem>(new MatchSystem());
            ServiceLocator.Register<IAssetsProvider>(new AssetsProvider());
            ServiceLocator.Register<ISceneSystem>(new SceneSystem());
            ServiceLocator.Register<IGameStateMachine>(new GameStateMachine());
            ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());

            ServiceLocator.Register<PlayerInfoBlockConfig>(_playerInfoBlockConfig);
            ServiceLocator.Register<LoadingScreenSystemConfig>(_loadingScreenSystemConfig);
            ServiceLocator.Register<HealthSystemConfig>(_healthSystemConfig);
            ServiceLocator.Register<ShootingSystemConfig>(_shootingSystemConfig);

        }

        private async UniTask InitServices()
        {
            _forTests.Initialize();
            
            ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateAndRegisterInfrastructure();
            
            ServiceLocator.Get<IInputService>().Initialize();
            ServiceLocator.Get<IPlayerGameplayInputHandler>().Initialize();

            ServiceLocator.Get<IEquipmentFactory>().Initialize();
            ServiceLocator.Get<IEquipment>().Initialize();
            ServiceLocator.Get<IEquipmentSystem>().Initialize();

            ServiceLocator.Get<ILoadingScreenSystem>().Initialize();
            ServiceLocator.Get<IDebriefingCanvasSystem>().Initialize();
            ServiceLocator.Get<IGameplayInfoCanvasSystem>().Initialize();
            ServiceLocator.Get<IInputCanvasSystem>().Initialize();
            ServiceLocator.Get<ISharedGameplayCanvasSystem>().Initialize();
            
            ServiceLocator.Get<ISpawnSystem>().Initialize();
            ServiceLocator.Get<IRespawnSystem>().Initialize();
            ServiceLocator.Get<IOperationSystem>().Initialize();
            ServiceLocator.Get<ITeamsSystem>().Initialize();
            ServiceLocator.Get<IMatchSystem>().Initialize();
            ServiceLocator.Get<DeathmatchPointsRuleSystem>().Initialize();
            ServiceLocator.Get<IPointsSystem>().Initialize();
            ServiceLocator.Get<IAssetsProvider>().Initialize();
            ServiceLocator.Get<ISceneSystem>().Initialize();
            ServiceLocator.Get<IGameStateMachine>().Initialize();
        }
    }
}