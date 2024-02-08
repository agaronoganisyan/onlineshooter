using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;

namespace Gameplay.EffectsLogic.HitEffectLogic
{
    public class HitEffectFactory : ObjectFactory<Effect>, IHitEffectFactory
    {
        private HitEffectFactoryConfig _factoryConfig;
        
        public override void Initialize()
        {
            _factoryConfig = ServiceLocator.Get<HitEffectFactoryConfig>();

            _pool = new ObjectPool<Effect>();
            _pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
    }
}