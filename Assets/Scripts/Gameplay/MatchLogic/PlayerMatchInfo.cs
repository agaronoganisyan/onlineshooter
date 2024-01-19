namespace Gameplay.MatchLogic
{
    public class PlayerMatchInfo : IPlayerMatchInfo
    {
        public TeamType TeamType => _teamType;
        private TeamType _teamType;

        public void Setup(TeamType teamType)
        {
            _teamType = teamType;
        }
    }
}