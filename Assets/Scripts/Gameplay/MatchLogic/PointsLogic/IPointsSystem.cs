using System;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.PointsLogic
{
    public interface IPointsSystem : IService
    {
        event Action<MatchResultType> OnMatchResultDetermined;
        event Action<int> OnPlayerTeamPointsIncreased;
        event Action<int> OnEnemyTeamPointsIncreased;
        event Action OnPointsAreReset;
        void Initialize();
        UniTask Prepare();
        void Cleanup();
    }
}