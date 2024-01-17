using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Async;

namespace Infrastructure.GameStateMachineLogic
{
    public class GameStateMachine : IGameStateMachine
    {
        private IStateMachine<GameState> _stateMachine;

        public void Initialize()
        {
            _stateMachine = new AsyncStateMachine<GameState>();

            _stateMachine.Add(GameState.Lobby, new Lobby(_stateMachine));
            _stateMachine.Add(GameState.Match, new Match(_stateMachine));

            _stateMachine.Start(GameState.Lobby);
        }

        public void SwitchState(GameState gameState)
        {
            _stateMachine.TransitToState(gameState);
        }
    }
}