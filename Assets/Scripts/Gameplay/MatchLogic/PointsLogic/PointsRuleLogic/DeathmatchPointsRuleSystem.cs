using System;
using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic.PointsRuleLogic
{
    public class DeathmatchPointsRuleSystem : IPointsRuleSystem
    {
        public event Action<TeamType, int> OnTeamPointsIncreased;
        
        private ITeamsSystem _teamsSystem;

        private int _firstTeamPointsCount;
        private int _secondTeamPointsCount;
        
        public void Initialize()
        {
            _teamsSystem = ServiceLocator.Get<ITeamsSystem>();
        }

        public void Prepare()
        {
            for (int i = 0; i < _teamsSystem.FirstTeamMembers.Count; i++)
            {
                _teamsSystem.FirstTeamMembers[i].OnDied += IncreaseSecondTeamPoints;
            }
            
            for (int i = 0; i < _teamsSystem.SecondTeamMembers.Count; i++)
            {
                _teamsSystem.SecondTeamMembers[i].OnDied += IncreaseFirstTeamPoints;
            }
        }

        public void Cleanup()
        {
            _firstTeamPointsCount = 0;
            _secondTeamPointsCount = 0;
            
            for (int i = 0; i < _teamsSystem.FirstTeamMembers.Count; i++)
            {
                _teamsSystem.FirstTeamMembers[i].OnDied -= IncreaseSecondTeamPoints;
            }
            
            for (int i = 0; i < _teamsSystem.SecondTeamMembers.Count; i++)
            {
                _teamsSystem.SecondTeamMembers[i].OnDied -= IncreaseFirstTeamPoints;
            }
        }

        public TeamType GetWinningTeam()
        {
            if (_firstTeamPointsCount > _secondTeamPointsCount) return TeamType.First;
            else if (_firstTeamPointsCount < _secondTeamPointsCount) return TeamType.Second;
            else return TeamType.None; 
        }

        private void IncreaseFirstTeamPoints()
        {
            _firstTeamPointsCount++;
            OnTeamPointsIncreased?.Invoke(TeamType.First, _firstTeamPointsCount);
        }

        private void IncreaseSecondTeamPoints()
        {
            _secondTeamPointsCount++;
            OnTeamPointsIncreased?.Invoke(TeamType.Second, _secondTeamPointsCount);
        }
    }
}