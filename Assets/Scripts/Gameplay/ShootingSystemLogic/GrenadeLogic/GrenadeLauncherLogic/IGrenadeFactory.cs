using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public interface IGrenadeFactory : IService
    {
        void Initialize();
        Grenade Get();
        void ReturnAllObjectToPool();
    }
}