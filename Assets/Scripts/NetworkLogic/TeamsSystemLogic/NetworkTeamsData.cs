using Fusion;
using Gameplay.MatchLogic.TeamsLogic;

namespace NetworkLogic.TeamsSystemLogic
{
    public struct NetworkTeamsData : INetworkStruct
    {
        [Networked][Capacity(5)] public NetworkDictionary<PlayerRef, PlayerData> _firstTeamMembers => default;
        [Networked][Capacity(5)] public NetworkDictionary<PlayerRef, PlayerData> _secondTeamMembers  => default;

        public void AddUnitToTeam(PlayerRef player, PlayerData playerData)
        {
            if (_firstTeamMembers.Count > _secondTeamMembers.Count)
            {
                _secondTeamMembers.Add(player, playerData);
            }
            else
            {
                _firstTeamMembers.Add(player, playerData);
            }
        }

        public void RemoveUnitFromTeam(PlayerRef player)
        {
            if (_firstTeamMembers.ContainsKey(player))_firstTeamMembers.Remove(player);
            else if (_secondTeamMembers.ContainsKey(player))  _secondTeamMembers.Remove(player);
        }

        public bool IsTeamsReady()
        {
            return _firstTeamMembers.Count > 0 && _secondTeamMembers.Count > 0;
        }
        
        public TeamType GetPlayerTeam(PlayerRef player)
        {
            if (_firstTeamMembers.ContainsKey(player)) return TeamType.First;
            else if (_secondTeamMembers.ContainsKey(player)) return TeamType.Second;
            else return TeamType.None;
        }
        
        public PlayerData GetPlayerData(PlayerRef player)
        {
            TeamType playerTeam = GetPlayerTeam(player);
            if (playerTeam == TeamType.First) return _firstTeamMembers.Get(player);
            else return _secondTeamMembers.Get(player);
        }
        
        public PlayerData SetPlayerData(PlayerRef player, PlayerData data)
        {
            TeamType playerTeam = GetPlayerTeam(player);
            if (playerTeam == TeamType.First) return _firstTeamMembers.Set(player,data);
            else return _secondTeamMembers.Set(player,data);
        }
    }
}