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
        void Initialize();
        UniTask Prepare();
        void Cleanup();
    }
}