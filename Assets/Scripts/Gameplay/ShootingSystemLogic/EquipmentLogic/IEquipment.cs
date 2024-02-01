using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    public interface IEquipment : IService
    {
        event Action OnEquipmentChanged;
        event Action OnCurrentWeaponFired;
        event Action<int,int> OnCurrentWeaponAmmoChanged;
        event Action OnCurrentWeaponReloadingRequired;
        event Action OnCurrentWeaponReloadingStarted;
        event Action OnCurrentWeaponReloadingFinished;
        event Action OnWeaponSwitchingStarted;
        event Action<WeaponConfig> OnCurrentWeaponChanged;
        event Action OnWeaponSwitchingFinished;
        event Action OnGrenadeLaunchingStarted;
        event Action OnGrenadeLaunchingFinished;
        WeaponConfig CurrentWeaponInfo { get; }
        WeaponConfig NextWeaponInfo { get; }
        WeaponConfig GetWeaponInfoByType(WeaponType type);
        GrenadeLauncher CurrentGrenadeLauncher { get; }
        void Initialize(Weapon firstWeapon, Weapon secondWeapon, GrenadeLauncher grenade);
        void Prepare(IEquipmentContainer equipmentContainer);
        void ReloadWeapon();
        void SwitchWeapon();
        void WeaponFire();
        void StartWeaponReloading();
        void StartWeaponSwitching();
        void StartGrenadeLaunching();
        void FinishWeaponReloading();
        void FinishWeaponSwitching();
        void FinishGrenadeLaunching();
        bool IsWeaponRequiredReloading();
        bool IsWeaponReloadingPossible();
        bool IsWeaponReadyToFire(Transform target, float minAngleToStartingShooting);
    }
}