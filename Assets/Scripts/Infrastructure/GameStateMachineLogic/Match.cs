using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.EffectsLogic;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.PointsLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.TaskLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Gameplay.UILogic.DebriefingCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputCanvasLogic;
using InputLogic.InputServiceLogic;
using NetworkLogic;
using NetworkLogic.MatchLogic;
using NetworkLogic.PlayerFactory;

namespace Infrastructure.GameStateMachineLogic
{
    public class Match : GameBaseState<GameState>
    {
        private INetworkManager _networkManager;
        private INetworkMatchHandlerFactory _networkMatchHandlerFactory;
        private INetworkMatchHandler _networkMatchHandler;
        private IPlayerFactory _playerFactory;
        private ISceneSystem _sceneSystem;
        private IOperationSystem _operationSystem;
        private ITeamsSystem _teamsSystem;
        private IPointsSystem _pointsSystem;
        private IMatchTaskSystem _matchTaskSystem;
        private IMatchSystem _matchSystem;
        private IEquipmentSystem _equipmentSystem;
        private ISpawnSystem _spawnSystem;
        private IPlayerSystem _playerSystem;
        private IPlayerMatchInfo _playerMatchInfo;

        private ProfileSettingsConfig _profileSettingsConfig;
        private OperationConfig _currentOperation;

        //Canvases
        private IInputCanvasSystem _inputCanvas;
        private IGameplayInfoCanvasSystem _gameplayInfoCanvas;
        private ISharedGameplayCanvasSystem _sharedGameplayCanvas;
        private IDebriefingCanvasSystem _debriefingCanvas;

        //Factories
        private IBulletFactory _bulletFactory;
        private IGrenadeFactory _grenadeFactory;
        private ISharedGameplayCanvasObjectFactory _sharedGameplayCanvasObjectFactory;
        private IEffectsFactory _effectsFactory;
        
        public Match(IStateMachine<GameState> stateMachine) : base(stateMachine)
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkMatchHandlerFactory = ServiceLocator.Get<INetworkMatchHandlerFactory>();

            _playerFactory = ServiceLocator.Get<IPlayerFactory>();
            _sceneSystem = ServiceLocator.Get<ISceneSystem>();
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
            _teamsSystem = ServiceLocator.Get<ITeamsSystem>();
            _pointsSystem = ServiceLocator.Get<IPointsSystem>();
            _matchTaskSystem = ServiceLocator.Get<IMatchTaskSystem>();
            _equipmentSystem = ServiceLocator.Get<IEquipmentSystem>();
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            
            _profileSettingsConfig = ServiceLocator.Get<ProfileSettingsConfig>();
            
            _inputCanvas = ServiceLocator.Get<IInputCanvasSystem>();
            _gameplayInfoCanvas = ServiceLocator.Get<IGameplayInfoCanvasSystem>();
            _sharedGameplayCanvas = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            _debriefingCanvas = ServiceLocator.Get<IDebriefingCanvasSystem>();
            
            _bulletFactory = ServiceLocator.Get<IBulletFactory>();
            _grenadeFactory = ServiceLocator.Get<IGrenadeFactory>();
            _sharedGameplayCanvasObjectFactory = ServiceLocator.Get<ISharedGameplayCanvasObjectFactory>();
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();
        }
        
        public override async UniTask Enter()
        {
            await _networkManager.ConnectToGameRoom();
            await _networkMatchHandlerFactory.Register();
            _networkMatchHandler = ServiceLocator.Get<INetworkMatchHandler>();

            _networkMatchHandler.SetPlayerInfo(_networkManager.NetworkRunner.LocalPlayer, _profileSettingsConfig.GetNickname());
            _playerMatchInfo.Setup(_networkMatchHandler.NetworkTeamsData.GetPlayerTeam(_networkManager.NetworkRunner.LocalPlayer),_profileSettingsConfig.GetNickname());
            _currentOperation = await _operationSystem.GetOperation();
            _matchSystem.Prepare();
            await _matchSystem.WaitingPlayers();
            
            await _sceneSystem.LoadScene(_currentOperation.Scene);
            await _equipmentSystem.Prepare();
            await _playerFactory.Create();
            //await _teamsSystem.WaitPlayers();
            
            _pointsSystem.Prepare(_currentOperation.Type);
            _matchTaskSystem.Prepare();
            await _spawnSystem.WaitSpawnPoints();

            _spawnSystem.Spawn();
            _inputService.SetInputMode(InputMode.Gameplay);

            _matchSystem.Start(_currentOperation.Duration);
            
            _sharedGameplayCanvas.Show();
            _sharedGameplayCanvas.StartUpdating();
            _inputCanvas.Show();
            _gameplayInfoCanvas.Show();
            
            _loadingCanvas.Hide();
        }
        
        public override async UniTask Exit()
        {
            await _loadingCanvas.Show();
            await _sceneSystem.UnloadScene();
            _equipmentSystem.ResetEquipment();
            _matchSystem.Cleanup();
            _pointsSystem.Cleanup();
            _matchTaskSystem.Cleanup();
            _teamsSystem.Cleanup();
            _spawnSystem.Cleanup();
            _operationSystem.UnloadOperation();

            _bulletFactory.ReturnAllObjectToPool();
            _grenadeFactory.ReturnAllObjectToPool();
            _sharedGameplayCanvasObjectFactory.ReturnAllObjectToPool();
            _effectsFactory.ReturnAllObjectToPool();

            _sharedGameplayCanvas.StopUpdating(); 
            _sharedGameplayCanvas.Hide();
            _inputCanvas.Hide();
            _gameplayInfoCanvas.Hide();
            _debriefingCanvas.Hide();
        }
    }
}