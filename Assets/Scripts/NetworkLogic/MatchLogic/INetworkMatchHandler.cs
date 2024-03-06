using System;
using Fusion;
using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchPointsLogic;
using NetworkLogic.TeamsSystemLogic;

namespace NetworkLogic.MatchLogic
{
    public interface INetworkMatchHandler : IService
    {
        event Action OnStarted;
        event Action OnFinished;
        event Action<string> OnMatchTimeGiven;
        event Action<string> OnRestOfMatchTimeChanged;
        event Action<NetworkTeamsPointsData> OnTeamPointsChanged;
        [Networked] public ref NetworkTeamsData NetworkTeamsData { get; }
        [Networked] NetworkBool IsReady { get; }
        void SetPlayerInfo(PlayerRef playerRef, string name);
        void IncreaseTeamPoints(TeamType teamType, int value);
        void StartMatch(float duration);
        TeamType GetWinningTeam();
    }
}