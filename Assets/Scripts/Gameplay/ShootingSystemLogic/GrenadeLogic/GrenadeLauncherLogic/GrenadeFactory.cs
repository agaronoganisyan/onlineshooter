using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public class GrenadeFactory : ObjectFactory<Grenade>, IGrenadeFactory
    {
        private GrenadeFactoryConfig _factoryConfig;
        
        public override void Initialize()
        {
            _factoryConfig = ServiceLocator.Get<GrenadeFactoryConfig>();

            _pool = new ObjectPool<Grenade>();
            _pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
    }
}