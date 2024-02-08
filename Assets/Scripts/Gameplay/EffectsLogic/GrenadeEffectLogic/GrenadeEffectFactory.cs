using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;

namespace Gameplay.EffectsLogic.GrenadeEffectLogic
{
    public class GrenadeEffectFactory : ObjectFactory<Effect>, IGrenadeEffectFactory
    {
        private GrenadeEffectFactoryConfig _factoryConfig;
        
        public override void Initialize()
        {
            _factoryConfig = ServiceLocator.Get<GrenadeEffectFactoryConfig>();

            _pool = new ObjectPool<Effect>();
            _pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
    }
}