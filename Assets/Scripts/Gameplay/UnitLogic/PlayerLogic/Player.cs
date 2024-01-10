using ConfigsLogic;
using Gameplay.HealthLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class Player : Unit
    {
        public HealthSystemWithCriticalThreshold HealthSystem => _healthSystem;
        protected HealthSystemWithCriticalThreshold _healthSystem;
        
        protected override void Awake()
        {
            base.Awake();
            _healthSystem = new HealthSystemWithCriticalThreshold(ServiceLocator.Get<HealthSystemConfig>());
            _hitBox.Initialize(_healthSystem);
            _healthSystem.Setup(1000);
        }
    }
}