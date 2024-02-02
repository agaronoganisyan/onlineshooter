using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public interface IBulletFactory : IService
    {
        void Initialize();
        Bullet Get();
        void ReturnAllObjectToPool();
    }
}