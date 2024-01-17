using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.OperationLogic;
using Infrastructure.SceneManagementLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputServiceLogic;

namespace Infrastructure.GameStateMachineLogic
{
    public class Match : GameBaseState<GameState>
    {
        private ISceneSystem _sceneSystem;
        private IOperationSystem _operationSystem;
        
        private OperationConfig _currentOperation;

        
        public Match(IStateMachine<GameState> stateMachine) : base(stateMachine)
        {
            _sceneSystem = ServiceLocator.Get<ISceneSystem>();
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
        }
        
        public override async UniTask Enter()
        {
            _currentOperation = await _operationSystem.GetOperation();
            await _sceneSystem.LoadScene(_currentOperation.Scene);
            
            _inputService.SetInputMode(InputMode.Gameplay);
            
            _loadingCanvas.Hide();
        }
        
        public override async UniTask Exit()
        {
            await _loadingCanvas.Show();
            await _sceneSystem.UnloadScene();

        }
    }
}