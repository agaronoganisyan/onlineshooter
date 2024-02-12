using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic.PointsLogic.PointsRuleLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.OperationLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic
{
    public class PointsSystem : IPointsSystem
    {
        public event Action<MatchResultType> OnMatchResultDetermined;
        public event Action<int> OnPlayerTeamPointsIncreased;
        public event Action<int> OnEnemyTeamPointsIncreased;
        public event Action OnPointsAreReset;

        private IPointsRuleSystem _pointsRuleSystem;
        private IMatchSystem _matchSystem;
        private IOperationSystem _operationSystem;
        private IPlayerMatchInfo _playerMatchInfo;
        private OperationConfig _operationConfig;
        
        public void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();

            _matchSystem.OnFinished += DetermineMatchResult;
        }

        private void DetermineMatchResult()
        {
            TeamType winningTeam = _pointsRuleSystem.GetWinningTeam();
            if (winningTeam == TeamType.None) OnMatchResultDetermined?.Invoke(MatchResultType.Draw);
            else if (IsPlayerTeam(winningTeam)) OnMatchResultDetermined?.Invoke(MatchResultType.Victory);
            else  OnMatchResultDetermined?.Invoke(MatchResultType.Defeat);
        }

        public async UniTask Prepare()
        {
            _operationConfig = await _operationSystem.GetOperation();
            SetRule(_operationConfig.Type);
        }

        public void Cleanup()
        {
            _pointsRuleSystem.Cleanup();
            _pointsRuleSystem.OnTeamPointsIncreased -= InvokePointsAction;
        }

        private void InvokePointsAction(TeamType teamType, int value)
        {
            if (IsPlayerTeam(teamType)) OnPlayerTeamPointsIncreased?.Invoke(value);
            else OnEnemyTeamPointsIncreased?.Invoke(value);
        }
        
        private void SetRule(OperationType operationType)
        {
            if (operationType == OperationType.Deathmatch)
            {
                _pointsRuleSystem = ServiceLocator.Get<DeathmatchPointsRuleSystem>();
                _pointsRuleSystem.Prepare();
                _pointsRuleSystem.OnTeamPointsIncreased += InvokePointsAction;
            }
            
            OnPointsAreReset?.Invoke();
        }

        private bool IsPlayerTeam(TeamType teamType)
        {
            return teamType == _playerMatchInfo.TeamType ? true : false;
        }
    }
}