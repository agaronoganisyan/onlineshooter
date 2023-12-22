using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.UnitLogic
{
    public interface IDamageable
    {
        void TakeDamage(RifleBullet bullet);
        void TakeDamage(Classic grenade);
    }
}