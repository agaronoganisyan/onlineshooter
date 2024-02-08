using Gameplay.EffectsLogic.GrenadeEffectLogic;
using Gameplay.EffectsLogic.HitEffectLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.EffectsLogic
{
    public class EffectsFactory : IEffectsFactory
    {
    private IHitEffectFactory _hitEffectFactory;
    private IGrenadeEffectFactory _grenadeEffectFactory;

    public void Initialize()
    {
        _hitEffectFactory = ServiceLocator.Get<IHitEffectFactory>();
        _grenadeEffectFactory = ServiceLocator.Get<IGrenadeEffectFactory>();
    }

    public Effect GetHitEffect()
    {
        return _hitEffectFactory.Get();
    }

    public Effect GetGrenadeEffect()
    {
        return _grenadeEffectFactory.Get();
    }

    public void ReturnAllObjectToPool()
    {
        _hitEffectFactory.ReturnAllObjectToPool();
        _grenadeEffectFactory.ReturnAllObjectToPool();
    }
    }
}