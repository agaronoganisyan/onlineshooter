namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class BulletFactory : ObjectFactory<Bullet>
    {
        public BulletFactory(Bullet prefab, int initialSize) : base(prefab, initialSize)
        {
        }
    }
}