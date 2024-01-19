using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.ShootingSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class Player : Unit
    {
        public HealthSystemWithCriticalThreshold HealthSystem => _playerHealthSystem;
        private HealthSystemWithCriticalThreshold _playerHealthSystem;
        private IShootingSystem _shootingSystem;
        private ISpawnSystem _spawnSystem;

        
        public override void Initialize()
        {
            base.Initialize();
            _shootingSystem = GetComponent<IShootingSystem>();
            
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            _playerHealthSystem = new HealthSystemWithCriticalThreshold(ServiceLocator.Get<HealthSystemConfig>());

            _playerHealthSystem.Setup(1000);
            _hitBox.Initialize(_playerHealthSystem);
            
            _shootingSystem.Initialize();
            
            _spawnSystem.OnSpawned += Prepare;
            
            Disable();
        }
        
        public override void Update()
        {
            base.Update();
            
            _shootingSystem.Tick();
        }
        
        public override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            base.Prepare(spawnPointInfo);
            _shootingSystem.Prepare();
        }
    }
}