using Gameplay.HealthLogic;

namespace Gameplay.UnitLogic
{
    public interface IUnitHitBox : IDamageable
    {
        void Initialize(HealthSystem healthSystem);
    }
}