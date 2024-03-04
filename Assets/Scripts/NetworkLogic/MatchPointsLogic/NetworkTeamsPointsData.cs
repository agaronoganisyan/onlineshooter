using Fusion;
using Gameplay.MatchLogic.TeamsLogic;

namespace NetworkLogic.MatchPointsLogic
{
    public struct NetworkTeamsPointsData : INetworkStruct
    {
        private int _firstTeamPoints; 
        private int _secondTeamPoints;
        
        public void IncreaseTeamPoints(TeamType teamType, int value)
        {
            if (teamType == TeamType.First) _firstTeamPoints += value;
            else _secondTeamPoints += value;
        }

        public int GetTeamPoints(TeamType teamType)
        {
            if (teamType == TeamType.First) return _firstTeamPoints;
            else return _secondTeamPoints;
        }
        
        public TeamType GetWinningTeam()
        {
            if (_firstTeamPoints > _secondTeamPoints) return TeamType.First;
            else if (_firstTeamPoints < _secondTeamPoints) return TeamType.Second;
            else return TeamType.None; 
        }
    }
}