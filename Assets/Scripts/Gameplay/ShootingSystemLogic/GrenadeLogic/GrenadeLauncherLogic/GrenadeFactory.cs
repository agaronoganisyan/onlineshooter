using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public class GrenadeFactory: ObjectFactory<Grenade>
    {
        public GrenadeFactory(Grenade prefab, int initialSize) : base(prefab, initialSize)
        {
        }
    }
}