using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.MapLogic
{
    public class Map : MonoBehaviour, IMap
    {
        private ISpawnSystem _spawnSystem;
        private ISpawnPointSystem _spawnPointsSystem;

        private void Awake()
        {
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
                
            _spawnPointsSystem = GetComponent<ISpawnPointSystem>();
        }

        private void Start()
        {
            _spawnPointsSystem.Initialize();
            _spawnSystem.SetSpawnPointSystem(_spawnPointsSystem);
        }
    }
}