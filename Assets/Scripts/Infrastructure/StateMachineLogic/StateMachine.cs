using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.StateMachineLogic
{
    public abstract class StateMachine<State> : IStateMachine<State> where State : Enum
    {
        private Dictionary<State, IState<State>> _states = new Dictionary<State, IState<State>>();
        private IState<State> _currentState;

        public void Add(State stateKey, IState<State> state)
        {
            _states.Add(stateKey, state);
        }

        public abstract void Initialize(State initialState);

        public void Tick()
        {
            _currentState.Update();
        }

        public abstract void TransitToState(State nextStateKey);

        public State GetCurrentState()
        {
            return _currentState.StateKey;
        }
        
        protected void SetCurrentState(IState<State> state)
        {
            _currentState = state;
        }

        protected TState GetState<TState>(State stateKey) where TState : class, IState<State>
        {
            if (_states.ContainsKey(stateKey)) return _states[stateKey] as TState;
            
            return null; 
        }
    }
}