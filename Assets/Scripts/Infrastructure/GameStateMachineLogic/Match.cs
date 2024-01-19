using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
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
        private IEquipmentSystem _equipmentSystem;
        private IMatchSystem _matchSystem;
        private ISpawnSystem _spawnSystem;

        
        private OperationConfig _currentOperation;

        //Canvases
        private IInputCanvas _inputCanvas;
        private IGameplayInfoCanvas _gameplayInfoCanvas;
        private ISharedGameplayCanvas _sharedGameplayCanvas;
        
        public Match(IStateMachine<GameState> stateMachine) : base(stateMachine)
        {
            _sceneSystem = ServiceLocator.Get<ISceneSystem>();
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
            _equipmentSystem = ServiceLocator.Get<IEquipmentSystem>();
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();

            
            _inputCanvas = ServiceLocator.Get<IInputCanvas>();
            _gameplayInfoCanvas = ServiceLocator.Get<IGameplayInfoCanvas>();
            _sharedGameplayCanvas = ServiceLocator.Get<ISharedGameplayCanvas>();
        }
        
        public override async UniTask Enter()
        {
            _currentOperation = await _operationSystem.GetOperation();
            await _sceneSystem.LoadScene(_currentOperation.Scene);
            await _equipmentSystem.Prepare();
            await _matchSystem.Prepare();
            
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
            _operationSystem.UnloadOperation();
            _equipmentSystem.ResetEquipment();
            _matchSystem.Cleanup();
            
            _sharedGameplayCanvas.Stop(); 
            _sharedGameplayCanvas.Hide();
            _inputCanvas.Hide();
            _gameplayInfoCanvas.Hide();
        }
    }
}