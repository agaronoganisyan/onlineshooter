using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.TeamsLogic
{
    public interface ITeamsSystem : IService
    {
        IReadOnlyList<Unit> FirstTeamMembers { get; }
        IReadOnlyList<Unit> SecondTeamMembers { get; }
        void Initialize();
        UniTask WaitPlayers();
        void AddUnitToTeam(Unit unit);
        void AddUnitToTeam(Unit unit, TeamType teamType);
        void Cleanup();
    }
}