using ConfigsLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class BulletFactory : ObjectFactory<Bullet>, IBulletFactory
    {
        private BulletFactoryConfig _factoryConfig;
        
        public override void Initialize()
        {
            _factoryConfig = ServiceLocator.Get<BulletFactoryConfig>();

            _pool = new ObjectPool<Bullet>();
            _pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
    }
}