using System;

namespace Infrastructure.StateMachineLogic.Simple
{
    public class SimpleStateMachine<State> : StateMachine<State> where State : Enum
    {
        private ISimpleState<State> _currentTypedState;
        
        public override void Start(State initialState)
        {
            _currentTypedState = GetState<ISimpleState<State>>(initialState);
            _currentTypedState.Enter();
            SetCurrentState(_currentTypedState);
        }

        public override void TransitToState(State nextStateKey)
        {
            if (nextStateKey.Equals(_currentTypedState.StateKey)) return;
            if (_currentTypedState != null)
            {
                _currentTypedState.Exit();
            }
            
            _currentTypedState = GetState<ISimpleState<State>>(nextStateKey);
            _currentTypedState.Enter();
            SetCurrentState(_currentTypedState);
        }
    }
}