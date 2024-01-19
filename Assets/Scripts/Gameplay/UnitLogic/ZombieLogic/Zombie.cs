using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.ServiceLogic;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class Zombie : Unit
    {
        protected SimpleHealthSystem _healthSystem;

        public override void Initialize()
        {
            base.Initialize();
            _healthSystem = new SimpleHealthSystem();
            _hitBox.Initialize(_healthSystem);
            _healthSystem.Setup(1000);
            //IPlayerInfoBlock playerInfoBlock = ServiceLocator.Get<ISharedGameplayCanvasObjectFactory>().GetPlayerBlockInfo(_healthSystem,_transform, _headTransform);
        }
    }
}