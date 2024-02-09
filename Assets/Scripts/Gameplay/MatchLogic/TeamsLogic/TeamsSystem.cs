using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.TeamsLogic
{
    public enum TeamType
    {
        None,
        First,
        Second
    }
    
    public class TeamsSystem : ITeamsSystem
    {
        private Player _player;
        private IPlayerMatchInfo _playerMatchInfo;

        private readonly TimeSpan _playersWaitingFrequency = TimeSpan.FromSeconds(1);

        public IReadOnlyList<Unit> FirstTeamMembers => _firstTeamMembers;
        private List<Unit> _firstTeamMembers = new List<Unit>();
        public IReadOnlyList<Unit> SecondTeamMembers => _secondTeamMembers;
        private List<Unit> _secondTeamMembers = new List<Unit>();


        public void Initialize()
        {
            _player = ServiceLocator.Get<Player>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
        }

        public async UniTask WaitPlayers()
        {
            while (!IsTeamsReady())
            {
                await UniTask.Delay(_playersWaitingFrequency);
            }
            
            //await UniTask.WaitUntil(IsTeamsReady);
            
            _playerMatchInfo.Setup(GetPlayerTeamType());
        }

        public void AddUnitToTeam(Unit unit)
        {
            if (_firstTeamMembers.Count > _secondTeamMembers.Count)
            {
                _secondTeamMembers.Add(unit);
            }
            else
            {
                _firstTeamMembers.Add(unit);
            }
        }

        public void Cleanup()
        {
            _firstTeamMembers.Clear();
            _secondTeamMembers.Clear();
        }

        private bool IsTeamsReady()
        {
            return _firstTeamMembers.Count > 0 && _secondTeamMembers.Count > 0;
        }
        
        private TeamType GetPlayerTeamType()
        {
            if (_firstTeamMembers.Contains(_player)) return TeamType.First;
            else return TeamType.Second;
        }
    }
}