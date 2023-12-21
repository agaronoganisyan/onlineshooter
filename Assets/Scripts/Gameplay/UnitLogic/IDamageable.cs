using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.UnitLogic
{
    public interface IDamageable
    {
        void TakeDamage(RifleBullet bullet);
    }
}