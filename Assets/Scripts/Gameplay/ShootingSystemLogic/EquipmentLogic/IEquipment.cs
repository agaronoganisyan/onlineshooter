using System;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    public interface IEquipment : IService
    {
        event Action OnCurrentWeaponReloadingStarted;
        event Action OnCurrentWeaponReloadingFinished;
        event Action<WeaponType> OnCurrentWeaponChanged;
        Weapon CurrentWeapon { get; }
        Weapon NextWeapon { get; }
        Weapon GetWeaponByType(WeaponType type);
        GrenadeLauncher CurrentGrenadeLauncher { get; }
        public void SetUp(Weapon[] weapons, GrenadeLauncher grenadeLauncher);
        public void SwitchWeapon();
    }
}