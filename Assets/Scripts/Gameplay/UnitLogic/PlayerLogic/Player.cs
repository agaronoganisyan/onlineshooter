using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class Player : Unit
    {
        public HealthSystemWithCriticalThreshold HealthSystem => _playerHealthSystem;
        protected HealthSystemWithCriticalThreshold _playerHealthSystem;
        
        protected override void Awake()
        {
            base.Awake();
            _playerHealthSystem = new HealthSystemWithCriticalThreshold(ServiceLocator.Get<HealthSystemConfig>());
            _hitBox.Initialize(_playerHealthSystem);
            _playerHealthSystem.Setup(1000);
            IPlayerInfoBlock playerInfoBlock = ServiceLocator.Get<ISharedGameplayCanvasObjectFactory>().GetPlayerBlockInfo(
                _playerHealthSystem,_transform, _headTransform);
        }
    }
}