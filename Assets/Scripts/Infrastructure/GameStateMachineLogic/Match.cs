using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.PointsLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.UILogic.DebriefingCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic.WeaponLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputCanvasLogic;
using InputLogic.InputServiceLogic;

namespace Infrastructure.GameStateMachineLogic
{
    public class Match : GameBaseState<GameState>
    {
        private ISceneSystem _sceneSystem;
        private IOperationSystem _operationSystem;
        private ITeamsSystem _teamsSystem;
        private IPointsSystem _pointsSystem;
        private IMatchSystem _matchSystem;
        private IEquipmentSystem _equipmentSystem;
        private ISpawnSystem _spawnSystem;
        private Player _player;
        
        private OperationConfig _currentOperation;

        //Canvases
        private IInputCanvasSystem _inputCanvas;
        private IGameplayInfoCanvasSystem _gameplayInfoCanvas;
        private ISharedGameplayCanvasSystem _sharedGameplayCanvas;
        private IDebriefingCanvasSystem _debriefingCanvas;

        public Match(IStateMachine<GameState> stateMachine) : base(stateMachine)
        {
            _sceneSystem = ServiceLocator.Get<ISceneSystem>();
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
            _teamsSystem = ServiceLocator.Get<ITeamsSystem>();
            _pointsSystem = ServiceLocator.Get<IPointsSystem>();
            _equipmentSystem = ServiceLocator.Get<IEquipmentSystem>();
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            _player = ServiceLocator.Get<Player>();
            
            _inputCanvas = ServiceLocator.Get<IInputCanvasSystem>();
            _gameplayInfoCanvas = ServiceLocator.Get<IGameplayInfoCanvasSystem>();
            _sharedGameplayCanvas = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            _debriefingCanvas = ServiceLocator.Get<IDebriefingCanvasSystem>();
        }
        
        public override async UniTask Enter()
        {
            _currentOperation = await _operationSystem.GetOperation();
            await _sceneSystem.LoadScene(_currentOperation.Scene);
            await _equipmentSystem.Prepare();
            await _matchSystem.Prepare();
            _teamsSystem.AddUnitToTeam(_player);
            await _teamsSystem.WaitPlayers();
            await _pointsSystem.Prepare();
            await _spawnSystem.WaitSpawnPoints();

            _spawnSystem.Spawn();
            _inputService.SetInputMode(InputMode.Gameplay);

            _matchSystem.Start();
            
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
            _teamsSystem.Cleanup();
            _spawnSystem.Cleanup();
            _operationSystem.UnloadOperation();

            _sharedGameplayCanvas.StopUpdating(); 
            _sharedGameplayCanvas.Hide();
            _inputCanvas.Hide();
            _gameplayInfoCanvas.Hide();
            _debriefingCanvas.Hide();
        }
    }
}