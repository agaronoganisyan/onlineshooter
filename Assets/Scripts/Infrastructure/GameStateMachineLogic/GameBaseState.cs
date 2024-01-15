using System;
using Cysharp.Threading.Tasks;
using Infrastructure.LoadingCanvasLogic;
using Infrastructure.ServiceLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Async;
using InputLogic.InputServiceLogic;

namespace Infrastructure.GameStateMachineLogic
{
    public enum GameState
    {
        None,
        Lobby,
        Match
    }
    
    public class GameBaseState<State> : IAsyncState<State> where State : Enum
    {
        public State StateKey { get; }
        
        protected readonly IStateMachine<GameState> _stateMachine;

        protected IInputService _inputService;
        
        protected ILoadingCanvas _loadingCanvas;

        protected GameBaseState(IStateMachine<GameState> stateMachine)
        {
            _stateMachine = stateMachine;

            _inputService = ServiceLocator.Get<IInputService>();
            _loadingCanvas = ServiceLocator.Get<ILoadingCanvas>();
        }

        public virtual async UniTask Enter()
        {
        }

        public virtual async UniTask Exit()
        {
        }
        
        public virtual void Update()
        {
        }
    }
}