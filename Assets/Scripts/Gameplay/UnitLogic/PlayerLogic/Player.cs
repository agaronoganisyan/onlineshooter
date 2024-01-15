using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.ShootingSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class Player : Unit
    {
        public HealthSystemWithCriticalThreshold HealthSystem => _playerHealthSystem;
        private HealthSystemWithCriticalThreshold _playerHealthSystem;
        private IPlayerController _playerController;
        private IShootingSystem _shootingSystem;
        
        public override void Initialize()
        {
            base.Initialize();
            _playerController = GetComponent<IPlayerController>();
            _shootingSystem = GetComponent<IShootingSystem>();
            _playerHealthSystem = new HealthSystemWithCriticalThreshold(ServiceLocator.Get<HealthSystemConfig>());

            _playerHealthSystem.Setup(1000);
            _hitBox.Initialize(_playerHealthSystem);
            
            _playerController.Initialize();
            _shootingSystem.Initialize();
        }
        
        public override void Update()
        {
            base.Update();
            
            _playerController.Tick();
            _shootingSystem.Tick();
        }
        
        public override void Prepare()
        {
            base.Prepare();
            _shootingSystem.Prepare();
        }
    }
}