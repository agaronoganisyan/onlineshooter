using System;

namespace Infrastructure.StateMachineLogic.Async
{
    public class AsyncStateMachine<State> : StateMachine<State> where State : Enum
    {
        private IAsyncState<State> _currentTypedState;
        
        public override async void Start(State initialState)
        {
            _currentTypedState = GetState<IAsyncState<State>>(initialState);
            await _currentTypedState.Enter();
            SetCurrentState(_currentTypedState);
        }

        public override async void TransitToState(State nextStateKey)
        {
            if (nextStateKey.Equals(_currentTypedState.StateKey)) return;
            if (_currentTypedState != null)
            {
                await _currentTypedState.Exit();
            }
            
            _currentTypedState = GetState<IAsyncState<State>>(nextStateKey);
            await _currentTypedState.Enter();
            SetCurrentState(_currentTypedState);
        }
    }
}