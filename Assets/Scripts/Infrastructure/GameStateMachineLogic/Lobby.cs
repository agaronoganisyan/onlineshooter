using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputServiceLogic;
using LobbyLogic;

namespace Infrastructure.GameStateMachineLogic
{
    public class Lobby : GameBaseState<GameState>
    {
        private ILobbyCanvasSystem _lobbyCanvas;

        public Lobby(IStateMachine<GameState> stateMachine) : base(stateMachine)
        {
            _lobbyCanvas = ServiceLocator.Get<ILobbyCanvasSystem>();
        }
        
        public override async UniTask Enter()
        {
            _inputService.SetInputMode(InputMode.UI);
            
            _lobbyCanvas.Show();
            _loadingCanvas.Hide();
        }
        
        public override async UniTask Exit()
        {
            await _loadingCanvas.Show();
            _lobbyCanvas.Hide();
        }
    }
}