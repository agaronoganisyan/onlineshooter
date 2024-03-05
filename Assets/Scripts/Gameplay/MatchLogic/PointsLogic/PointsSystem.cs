using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic.PointsLogic.PointsRuleLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchLogic;
using NetworkLogic.MatchPointsLogic;

namespace Gameplay.MatchLogic.PointsLogic
{
    public class PointsSystem : IPointsSystem
    {
        public event Action<MatchResultType> OnMatchResultDetermined;
        public event Action<int> OnPlayerTeamPointsChanged;
        public event Action<int> OnEnemyTeamPointsChanged;
        public event Action OnPointsAreReset;

        private IPointsRuleSystem _pointsRuleSystem;
        private IMatchSystem _matchSystem;
        private IPlayerMatchInfo _playerMatchInfo;
        private OperationConfig _operationConfig;
        private INetworkMatchHandler _networkMatchHandler;
        
        public void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();

            _matchSystem.OnFinished += DetermineMatchResult;
        }

        public void Prepare(OperationType operationType)
        {
            _networkMatchHandler = ServiceLocator.Get<INetworkMatchHandler>();
            _networkMatchHandler.OnTeamPointsChanged += TeamPointsIncreased;
            
            if (operationType == OperationType.Deathmatch)
            {
                _pointsRuleSystem = ServiceLocator.Get<DeathmatchPointsRuleSystem>();
                _pointsRuleSystem.OnTeamPointsIncreased += IncreaseTeamPoints;
                _pointsRuleSystem.Prepare();
            }
            
            OnPointsAreReset?.Invoke();
        }

        private void DetermineMatchResult()
        {
            TeamType winningTeam = _networkMatchHandler.GetWinningTeam();
            if (winningTeam == TeamType.None) OnMatchResultDetermined?.Invoke(MatchResultType.Draw);
            else if (IsPlayerTeam(winningTeam)) OnMatchResultDetermined?.Invoke(MatchResultType.Victory);
            else  OnMatchResultDetermined?.Invoke(MatchResultType.Defeat);
        }

        private void IncreaseTeamPoints(TeamType teamType, int value)
        {
            _networkMatchHandler.IncreaseTeamPoints(teamType, value);
        }

        private void TeamPointsIncreased(NetworkTeamsPointsData data)
        {
            OnPlayerTeamPointsChanged?.Invoke(data.GetTeamPoints(_playerMatchInfo.TeamType));
            OnEnemyTeamPointsChanged?.Invoke(data.GetTeamPoints(_playerMatchInfo.GetOppositeTeamType()));
        }

        public void Cleanup()
        {
            _networkMatchHandler.OnTeamPointsChanged -= TeamPointsIncreased;
            
            _pointsRuleSystem.Cleanup();
            _pointsRuleSystem.OnTeamPointsIncreased -= IncreaseTeamPoints;
        }

        private bool IsPlayerTeam(TeamType teamType)
        {
            return teamType == _playerMatchInfo.TeamType ? true : false;
        }
    }
}