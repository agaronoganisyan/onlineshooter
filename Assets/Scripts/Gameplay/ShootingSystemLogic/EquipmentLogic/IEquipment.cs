using System;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    public interface IEquipment
    {
        event Action<WeaponType> OnCurrentWeaponChanged;
        Weapon CurrentWeapon { get; }
        Weapon NextWeapon { get; }
        Weapon GetWeaponByType(WeaponType type);
        GrenadeLauncher CurrentGrenadeLauncher { get; }
        public void SetUp(Weapon[] weapons, GrenadeLauncher grenadeLauncher);
        public void SwitchWeapon();
    }
}