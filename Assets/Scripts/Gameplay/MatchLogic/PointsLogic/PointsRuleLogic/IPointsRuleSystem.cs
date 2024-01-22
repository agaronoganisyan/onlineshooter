using System;
using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic.PointsRuleLogic
{
    public interface IPointsRuleSystem : IService

    {
    event Action<TeamType, int> OnTeamPointsIncreased;
    void Initialize();
    void Prepare();
    void Cleanup();
    TeamType GetWinningTeam();
    }
}