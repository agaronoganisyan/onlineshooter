using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public class PlayerInfoBlockFactory : ObjectFactory<PlayerInfoBlock>, IPlayerInfoBlockFactory
    {
        private PlayerInfoBlockFactoryConfig _factoryConfig;
        
        public override void Initialize()
        {
            _factoryConfig = ServiceLocator.Get<PlayerInfoBlockFactoryConfig>();

            _pool = new ObjectPool<PlayerInfoBlock>();
            _pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
    }
}