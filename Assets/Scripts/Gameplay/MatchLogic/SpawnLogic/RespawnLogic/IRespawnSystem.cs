using System;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.SpawnLogic.RespawnLogic
{
    public interface IRespawnSystem : IService
    {
        event Action<string> OnRespawnTimeGiven;
        event Action<string> OnRestOfRespawnTimeChanged;
        event Action OnStarted;
        event Action OnFinished;
        event Action OnStopped;
        void Initialize();
    }
}