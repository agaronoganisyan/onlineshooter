using System;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.TaskLogic
{
    public interface IMatchTaskSystem : IService
    {
        event Action<string> OnTaskChanged;
        void Initialize();
        UniTask Prepare();
        void Cleanup();
    }
}