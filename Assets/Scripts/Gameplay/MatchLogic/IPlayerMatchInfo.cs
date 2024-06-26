using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic
{
    public interface IPlayerMatchInfo : IService
    {
        TeamType TeamType { get; }
        string Name { get; }
        void Setup(TeamType teamType, string name);
        TeamType GetOppositeTeamType();
    }
}