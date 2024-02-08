using Infrastructure.ServiceLogic;

namespace Gameplay.EffectsLogic
{
    public interface IEffectsFactory : IService
    {
        void Initialize();
        Effect GetHitEffect();
        Effect GetGrenadeEffect();
        void ReturnAllObjectToPool();
    }
}