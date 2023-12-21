using System;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public interface IStateMachine<State> where State : Enum
    {
        void Add(State stateKey, IState<State> state);
        void Start(State initialState);
        void Update();
        void TransitToState(State nextStateKey);
        State GetCurrentState();
    }
}