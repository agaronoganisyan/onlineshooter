using System;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.SpawnLogic
{
    public interface ISpawnSystem  : IService
    {
        event Action<SpawnPointInfo> OnSpawned; 
        void Initialize();
        void Spawn();
        void SetSpawnPointSystem(ISpawnPointSystem spawnPointSystem);
        UniTask WaitSpawnPoints();
        void Cleanup();
    }
}