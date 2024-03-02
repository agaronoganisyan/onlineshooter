using System;
using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.MatchLogic
{
    public interface INetworkMatchHandler : IService
    {
        event Action OnStarted;
        event Action OnFinished;
        event Action<string> OnMatchTimeGiven;
        event Action<string> OnRestOfMatchTimeChanged;
        [Networked] NetworkBool IsReady { get; }
        void StartMatch(float duration);
    }
}