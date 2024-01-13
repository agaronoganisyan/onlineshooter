using System;

namespace Infrastructure.StateMachineLogic
{
    public interface IStateMachine<State> where State : Enum
    {
        void Add(State stateKey, IState<State> state);
        void Start(State initialState);
        void Tick();
        void TransitToState(State nextStateKey);
        State GetCurrentState();
    }
}