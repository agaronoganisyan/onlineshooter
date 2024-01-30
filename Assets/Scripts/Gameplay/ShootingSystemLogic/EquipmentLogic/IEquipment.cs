using System;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    public interface IEquipment : IService
    {
        event Action OnEquipmentChanged;
        event Action OnCurrentWeaponFired;
        event Action OnCurrentWeaponReloadingStarted;
        event Action OnCurrentWeaponReloadingFinished;
        event Action OnWeaponSwitchingStarted;
        event Action OnWeaponSwitchingFinished;
        event Action<Weapon> OnCurrentWeaponChanged;
        event Action OnGrenadeLaunchingStarted;
        event Action OnGrenadeLaunchingFinished;
        Weapon CurrentWeapon { get; }
        Weapon NextWeapon { get; }
        Weapon GetWeaponByType(WeaponType type);
        GrenadeLauncher CurrentGrenadeLauncher { get; }
        void Initialize();
        public void Prepare(Weapon firstWeapon, Weapon secondWeapon, GrenadeLauncher grenadeLauncher);
        public void SwitchWeapon();
        void FinishWeaponSwitching();
        void FinishGrenadeLaunching();
    }
}