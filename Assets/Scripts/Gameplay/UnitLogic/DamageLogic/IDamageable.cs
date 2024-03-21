using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.UnitLogic.DamageLogic
{
    public interface IDamageable
    {
        bool IsCanTakeHit(HitInfo hitInfo);
        void TakeDamage(Bullet bullet);
        void TakeDamage(Grenade grenade);
    }
}