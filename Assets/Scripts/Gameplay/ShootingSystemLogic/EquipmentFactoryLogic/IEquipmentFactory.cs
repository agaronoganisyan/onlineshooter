using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentFactoryLogic
{
    public interface IEquipmentFactory : IService
    {
        void Initialize();
        UniTask<Weapon> GetWeapon(string address);
        UniTask<GrenadeLauncher> GetGrenadeLauncher(string address);
        void UnloadEquipment(string equipmentAddress);
    }
}