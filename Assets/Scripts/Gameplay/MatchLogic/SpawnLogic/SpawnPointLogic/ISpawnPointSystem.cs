using Gameplay.MatchLogic.TeamsLogic;

namespace Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic
{
    public interface ISpawnPointSystem
    {
        void Initialize();
        SpawnPointInfo GetSpawnPointInfo(TeamType teamType);
    }
}