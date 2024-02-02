using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.UnitLogic
{
    public interface IDamageable
    {
        void TakeDamage(Bullet bullet);
        void TakeDamage(Grenade grenade);
    }
}