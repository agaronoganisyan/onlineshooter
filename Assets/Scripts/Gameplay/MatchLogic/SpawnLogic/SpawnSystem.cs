using System;
using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.SpawnLogic
{
    public class SpawnSystem : ISpawnSystem

    {
        public event Action<SpawnPointInfo> OnSpawned;

        private ISpawnPointSystem _spawnPointSystem;
        private IPlayerMatchInfo _playerMatchInfo;
        
        private bool _isSpawnPointSystemGiven;
        
        private readonly TimeSpan _spawnPointsWaitingFrequency = TimeSpan.FromSeconds(1);
        
        public void Initialize()
        {
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
        }
        
        public void Spawn()
        {
            OnSpawned?.Invoke(_spawnPointSystem.GetSpawnPointInfo(_playerMatchInfo.TeamType));
        }

        public void SetSpawnPointSystem(ISpawnPointSystem spawnPointSystem)
        {
            _spawnPointSystem = spawnPointSystem;
            _isSpawnPointSystemGiven = true;
        }
        
        public async UniTask WaitSpawnPoints()
        {
            while (!_isSpawnPointSystemGiven)
            {
                await UniTask.Delay(_spawnPointsWaitingFrequency);
            }
        }

        public void Cleanup()
        {
            _spawnPointSystem = null;
            _isSpawnPointSystemGiven = false;
        }
    }
}