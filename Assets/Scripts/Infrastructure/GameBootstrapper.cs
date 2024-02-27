using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.EffectsLogic;
using Gameplay.EffectsLogic.GrenadeEffectLogic;
using Gameplay.EffectsLogic.HitEffectLogic;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.PointsLogic;
using Gameplay.MatchLogic.PointsLogic.PointsRuleLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.MatchLogic.TaskLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EquipmentFactoryLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Gameplay.UILogic.DebriefingCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.AssetManagementLogic;
using Infrastructure.GameFactoryLogic;
using Infrastructure.GameStateMachineLogic;
using Infrastructure.LoadingCanvasLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputCanvasLogic;
using InputLogic.InputServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using LobbyLogic;
using NetworkLogic;
using NetworkLogic.PlayerFactory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        //[SerializeField] private ForTests _forTests;
        
        [SerializeField] private NetworkManager _networkManager;
        
        [SerializeField] private ProfileSettingsConfig _profileSettingsConfig;
        [SerializeField] private ControlsSettingsConfig _controlsSettingsConfig;
        [SerializeField] private PlayerInfoBlockConfig _playerInfoBlockConfig;
        [SerializeField] private HealthSystemConfig _healthSystemConfig;
        [SerializeField] private LoadingScreenSystemConfig _loadingScreenSystemConfig;
        [SerializeField] private ShootingSystemConfig _shootingSystemConfig;
        [SerializeField] private BulletFactoryConfig _bulletFactoryConfig;
        [SerializeField] private GrenadeFactoryConfig _grenadeFactoryConfig;
        [SerializeField] private PlayerInfoBlockFactoryConfig _playerInfoBlockFactoryConfig;
        [SerializeField] private HitEffectFactoryConfig _hitEffectFactoryConfig;
        [SerializeField] private GrenadeEffectFactoryConfig _grenadeEffectFactoryConfig;
        private async void Awake()
        {
            Application.targetFrameRate = 60;
            
            RegisterServices();
            await InitServices();
        }

        private void RegisterServices()
        {
            //ServiceLocator.Register<ForTests>(_forTests);
            
            ServiceLocator.Register<INetworkManager>(_networkManager);
            
            ServiceLocator.Register<IPlayerSystem>(new PlayerSystem());
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
            
            ServiceLocator.Register<IEffectsFactory>(new EffectsFactory());
            ServiceLocator.Register<IHitEffectFactory>(new HitEffectFactory());
            ServiceLocator.Register<IGrenadeEffectFactory>(new GrenadeEffectFactory());   
            ServiceLocator.Register<ISharedGameplayCanvasObjectFactory>(new SharedGameplayCanvasObjectFactory());
            ServiceLocator.Register<IPlayerInfoBlockFactory>(new PlayerInfoBlockFactory());
            ServiceLocator.Register<IBulletFactory>(new BulletFactory());
            ServiceLocator.Register<IGrenadeFactory>(new GrenadeFactory());

            ServiceLocator.Register<PlayerHealthSystem>(new PlayerHealthSystem());
            
            ServiceLocator.Register<IPlayerMatchInfo>(new PlayerMatchInfo());
            ServiceLocator.Register<ISpawnSystem>(new SpawnSystem());
            ServiceLocator.Register<IRespawnSystem>(new RespawnSystem());
            ServiceLocator.Register<DeathmatchPointsRuleSystem>(new DeathmatchPointsRuleSystem());
            ServiceLocator.Register<IPointsSystem>(new PointsSystem());
            ServiceLocator.Register<IMatchTaskSystem>(new MatchTaskSystem());
            ServiceLocator.Register<IOperationSystem>(new OperationSystem());
            ServiceLocator.Register<ITeamsSystem>(new TeamsSystem());
            ServiceLocator.Register<IMatchSystem>(new MatchSystem());
            ServiceLocator.Register<IAssetsProvider>(new AssetsProvider());
            ServiceLocator.Register<ISceneSystem>(new SceneSystem());
            ServiceLocator.Register<IGameStateMachine>(new GameStateMachine());
            ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());
            ServiceLocator.Register<IPlayerFactory>(new PlayerFactory());

            
            ServiceLocator.Register<ProfileSettingsConfig>(_profileSettingsConfig);
            ServiceLocator.Register<ControlsSettingsConfig>(_controlsSettingsConfig);
            ServiceLocator.Register<PlayerInfoBlockConfig>(_playerInfoBlockConfig);
            ServiceLocator.Register<LoadingScreenSystemConfig>(_loadingScreenSystemConfig);
            ServiceLocator.Register<HealthSystemConfig>(_healthSystemConfig);
            ServiceLocator.Register<ShootingSystemConfig>(_shootingSystemConfig);
            ServiceLocator.Register<BulletFactoryConfig>(_bulletFactoryConfig);
            ServiceLocator.Register<GrenadeFactoryConfig>(_grenadeFactoryConfig);
            ServiceLocator.Register<PlayerInfoBlockFactoryConfig>(_playerInfoBlockFactoryConfig);
            ServiceLocator.Register<HitEffectFactoryConfig>(_hitEffectFactoryConfig);
            ServiceLocator.Register<GrenadeEffectFactoryConfig>(_grenadeEffectFactoryConfig);
        }

        private async UniTask InitServices()
        {
            //ServiceLocator.Get<ForTests>().Initialize();
            
            ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateAndRegisterInfrastructure();
            
            ServiceLocator.Get<INetworkManager>().Initialize();
            
            ServiceLocator.Get<IPlayerFactory>().Initialize();
            ServiceLocator.Get<IPlayerSystem>().Initialize();
            
            ServiceLocator.Get<IInputService>().Initialize();
            ServiceLocator.Get<IPlayerGameplayInputHandler>().Initialize();

            ServiceLocator.Get<IEquipmentFactory>().Initialize();
            ServiceLocator.Get<IEquipmentSystem>().Initialize();

            ServiceLocator.Get<ILoadingScreenSystem>().Initialize();
            ServiceLocator.Get<IDebriefingCanvasSystem>().Initialize();
            ServiceLocator.Get<IGameplayInfoCanvasSystem>().Initialize();
            ServiceLocator.Get<IInputCanvasSystem>().Initialize();
            ServiceLocator.Get<ISharedGameplayCanvasSystem>().Initialize();
            
            ServiceLocator.Get<IHitEffectFactory>().Initialize();
            ServiceLocator.Get<IGrenadeEffectFactory>().Initialize();
            ServiceLocator.Get<IEffectsFactory>().Initialize();
            ServiceLocator.Get<IBulletFactory>().Initialize();
            ServiceLocator.Get<IGrenadeFactory>().Initialize();
            ServiceLocator.Get<IPlayerInfoBlockFactory>().Initialize();
            ServiceLocator.Get<ISharedGameplayCanvasObjectFactory>().Initialize();
            
            ServiceLocator.Get<ISpawnSystem>().Initialize();
            ServiceLocator.Get<IRespawnSystem>().Initialize();
            ServiceLocator.Get<IOperationSystem>().Initialize();
            ServiceLocator.Get<ITeamsSystem>().Initialize();
            ServiceLocator.Get<IMatchSystem>().Initialize();
            ServiceLocator.Get<DeathmatchPointsRuleSystem>().Initialize();
            ServiceLocator.Get<IPointsSystem>().Initialize();
            ServiceLocator.Get<IMatchTaskSystem>().Initialize();
            ServiceLocator.Get<IAssetsProvider>().Initialize();
            ServiceLocator.Get<ISceneSystem>().Initialize();
            ServiceLocator.Get<IGameStateMachine>().Initialize();
        }
    }
}