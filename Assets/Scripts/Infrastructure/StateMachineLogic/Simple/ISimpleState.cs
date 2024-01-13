using System;

namespace Infrastructure.StateMachineLogic.Simple
{
    public interface ISimpleState<State> : IState<State> where State : Enum
    {
        void Enter();
        void Exit();
    }
}