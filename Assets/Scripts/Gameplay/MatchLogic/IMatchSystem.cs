using System;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic
{
    public interface IMatchSystem : IService
    {
        event Action OnStarted;
        event Action OnFinished;
        event Action<string> OnMatchTimeGiven;
        event Action<string> OnRestOfMatchTimeChanged;
        void Initialize();
        UniTask Prepare();
        void Start();
        void Cleanup();    }
}