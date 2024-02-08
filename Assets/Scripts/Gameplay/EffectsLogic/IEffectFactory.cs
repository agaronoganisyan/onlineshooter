namespace Gameplay.EffectsLogic
{
    public interface IEffectFactory
    {
        void Initialize();
        Effect Get();
        void ReturnAllObjectToPool();
    }
}