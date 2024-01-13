using System;
using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachineLogic.Async
{
    public interface IAsyncState<State> : IState<State> where State : Enum
    {
        UniTask Enter();
        UniTask Exit();
    }
}