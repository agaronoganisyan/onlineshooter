using System;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public interface IState<State> where State : Enum
    {
        State StateKey { get; }
        void Enter();
        void Exit();
        void Update();
    }
}