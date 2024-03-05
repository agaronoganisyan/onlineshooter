using System;
using Gameplay.MatchLogic.TeamsLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic.PointsRuleLogic
{
    public class DeathmatchPointsRuleSystem : IPointsRuleSystem
    {
        public event Action<TeamType, int> OnTeamPointsIncreased;
        
        private IPlayerSystem _playerSystem;
        private IPlayerMatchInfo _playerMatchInfo;
        
        public void Initialize()
        {
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
        }

        public void Prepare()
        {
            _playerSystem.Player.OnDied += IncreaseTeamPoints;
        }

        public void Cleanup()
        {
            _playerSystem.Player.OnDied -= IncreaseTeamPoints;
        }

        private void IncreaseTeamPoints()
        {
            OnTeamPointsIncreased?.Invoke(
                _playerMatchInfo.TeamType == TeamType.First ? TeamType.Second : TeamType.First,
                1);
        }
    }
}