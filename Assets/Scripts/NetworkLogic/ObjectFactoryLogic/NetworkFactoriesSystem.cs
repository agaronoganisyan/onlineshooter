using Gameplay.EffectsLogic;
using Gameplay.EffectsLogic.GrenadeEffectLogic;
using Gameplay.EffectsLogic.HitEffectLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.ObjectFactoryLogic
{
    public class NetworkFactoriesSystem : INetworkFactoriesSystem
    {
        private IHitEffectFactory _hitEffectFactory;
        private IGrenadeEffectFactory _grenadeEffectFactory;
        private IEffectsFactory _effectsFactory;
        private IBulletFactory _bulletFactory;
        private IGrenadeFactory _grenadeFactory;
        
        public void Initialize()
        {
            _hitEffectFactory = ServiceLocator.Get<IHitEffectFactory>();
            _grenadeEffectFactory = ServiceLocator.Get<IGrenadeEffectFactory>();
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();
            _bulletFactory = ServiceLocator.Get<IBulletFactory>();
            _grenadeFactory = ServiceLocator.Get<IGrenadeFactory>();
        }

        public void Prepare()
        {
            _hitEffectFactory.Initialize();
            _grenadeEffectFactory.Initialize();
            _effectsFactory.Initialize();
            _bulletFactory.Initialize();
            _grenadeFactory.Initialize();
        }
    }
}