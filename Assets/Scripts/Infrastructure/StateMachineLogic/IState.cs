using System;

namespace Infrastructure.StateMachineLogic
{
    public interface IState<State> where State : Enum
    {
        State StateKey { get; }
        void Update();
    }
}