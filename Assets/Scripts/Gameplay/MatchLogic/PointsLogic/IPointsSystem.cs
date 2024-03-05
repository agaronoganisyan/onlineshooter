using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic
{
    public interface IPointsSystem : IService
    {
        event Action<MatchResultType> OnMatchResultDetermined;
        event Action<int> OnPlayerTeamPointsChanged;
        event Action<int> OnEnemyTeamPointsChanged;
        event Action OnPointsAreReset;
        void Initialize();
        void Prepare(OperationType operationType);
        void Cleanup();
    }
}