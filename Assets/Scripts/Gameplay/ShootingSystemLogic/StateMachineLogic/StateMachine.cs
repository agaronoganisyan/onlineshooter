using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class StateMachine<State> : IStateMachine<State> where State : Enum
    {
        private Dictionary<State, IState<State>> _states = new Dictionary<State, IState<State>>();
        private IState<State> _currentState;

        public void Add(State stateKey, IState<State> state)
        {
            _states.Add(stateKey, state);
        }

        public void Start(State initialState)
        {
            _currentState = _states[initialState];
            _currentState.Enter();

            Debug.Log($"CURRENT STATE {_currentState.StateKey}");
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void TransitToState(State nextStateKey)
        {
            if (nextStateKey.Equals(_currentState.StateKey)) return;
            if (_currentState != null) _currentState.Exit();
            
            Debug.Log($"PREVIOUS STATE {_currentState.StateKey}");
            
            _currentState = _states[nextStateKey];
            _currentState.Enter();
            
            Debug.Log($"CURRENT STATE {_currentState.StateKey}");

        }

        public State GetCurrentState()
        {
            return _currentState.StateKey;
        }
    }
}