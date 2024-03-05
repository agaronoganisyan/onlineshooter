using Gameplay.MatchLogic.TeamsLogic;

namespace Gameplay.MatchLogic
{
    public class PlayerMatchInfo : IPlayerMatchInfo
    {
        public TeamType TeamType => _teamType;
        private TeamType _teamType;
        public string Name => _name;
        private string _name;

        public void Setup(TeamType teamType, string name)
        {
            _teamType = teamType;
            _name = name;
        }
        
        public TeamType GetOppositeTeamType()
        {
            return _teamType == TeamType.First ? TeamType.Second : TeamType.First;
        }
    }
}