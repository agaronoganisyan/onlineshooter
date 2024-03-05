using System;
using Fusion;
using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchPointsLogic;

namespace NetworkLogic.MatchLogic
{
    public interface INetworkMatchHandler : IService
    {
        event Action OnStarted;
        event Action OnFinished;
        event Action<string> OnMatchTimeGiven;
        event Action<string> OnRestOfMatchTimeChanged;
        event Action<NetworkTeamsPointsData> OnTeamPointsChanged;

        [Networked] NetworkBool IsReady { get; }
        void IncreaseTeamPoints(TeamType teamType, int value);
        void StartMatch(float duration);
        TeamType GetWinningTeam();
    }
}