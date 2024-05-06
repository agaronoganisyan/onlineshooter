using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UnitLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    [Serializable]
    public class Equipment : IEquipment
    {
        public event Action OnEquipmentChanged;
        public event Action OnCurrentWeaponFired;
        public event Action<int,int> OnCurrentWeaponAmmoChanged;
        public event Action OnCurrentWeaponReloadingRequired;
        public event Action OnCurrentWeaponReloadingStarted;
        public event Action OnCurrentWeaponReloadingFinished;
        public event Action OnWeaponSwitchingStarted;
        public event Action<WeaponConfig> OnCurrentWeaponChanged;
        public event Action OnWeaponSwitchingFinished;
        public event Action OnGrenadeLaunchingStarted;
        public event Action OnGrenadeLaunchingFinished;
        
        public WeaponConfig CurrentWeaponInfo => _currentWeapon.Config;
        private Weapon _currentWeapon;
        public WeaponConfig NextWeaponInfo => _nextWeapon.Config;
        private Weapon _nextWeapon;
        
        private Weapon[] _weapons = new Weapon[2];
        
        private GrenadeLauncher _currentGrenadeLauncher;

        private int _currentWeaponID;
        private int _weaponsCount;
        
        public void Initialize(Weapon firstWeapon, Weapon secondWeapon, GrenadeLauncher grenade)
        {
            if (_currentWeapon != null) UnsubscribeFromThisWeapon(_currentWeapon);
            
            _weapons[0] = firstWeapon;
            _weapons[1] = secondWeapon;
            _currentGrenadeLauncher = grenade;

            _currentWeaponID = 0;
            _weaponsCount = _weapons.Length;
            
            SetWeapon(_currentWeaponID);
            SubscribeToThisWeapon(_currentWeapon);
            
            OnEquipmentChanged?.Invoke();
        }

        public void Prepare(Unit unit, IEquipmentContainer equipmentContainer)
        {
            _currentWeapon.Initialize(unit, equipmentContainer);
            _nextWeapon.Initialize(unit, equipmentContainer);
            _currentGrenadeLauncher.Initialize(unit, equipmentContainer);
            
            _currentWeapon.Draw();
            _nextWeapon.LayDown();
        }

        public void Reset()
        {
            UnsubscribeFromThisWeapon(_currentWeapon);
            
            _currentWeaponID = 0;
            SetWeapon(_currentWeaponID);
            SubscribeToThisWeapon(_currentWeapon);
            
            _currentWeapon.Draw();
            _nextWeapon.LayDown();
            
            OnEquipmentChanged?.Invoke();
        }

        public void ReloadWeapon()
        {
            _currentWeapon.FinishReloading();
        }

        public void SwitchWeapon()
        {
            UnsubscribeFromThisWeapon(_currentWeapon);
            
            _currentWeaponID = GetNextWeaponID(_currentWeaponID);
            SetWeapon(_currentWeaponID);
            SubscribeToThisWeapon(_currentWeapon);
            
            _currentWeapon.Draw();
            _nextWeapon.LayDown();
        }

        public void WeaponFire()
        {
            _currentWeapon.Fire();
        }

        public void StartWeaponReloading()
        {
            OnCurrentWeaponReloadingStarted?.Invoke();
        }
        
        public void StartWeaponSwitching()
        {
            OnWeaponSwitchingStarted?.Invoke();
        }

        public void StartGrenadeLaunching()
        {
            OnGrenadeLaunchingStarted?.Invoke();   
        }
        
        public void FinishWeaponReloading()
        {
            OnCurrentWeaponReloadingFinished?.Invoke();
        }

        public void FinishWeaponSwitching()
        {
            OnWeaponSwitchingFinished?.Invoke();
        }

        public void FinishGrenadeLaunching()
        {
            OnGrenadeLaunchingFinished?.Invoke();
        }

        public bool IsWeaponRequiredReloading()
        {
            return _currentWeapon.IsRequiredReloading();
        }

        public bool IsWeaponReloadingPossible()
        {
            return _currentWeapon.IsReloadingPossible();
        }

        public bool IsWeaponReadyToFire(Transform target, float minAngleToStartingShooting)
        {
            return _currentWeapon.IsReadyToFire(target,minAngleToStartingShooting);
        }

        public void LaunchGrenade(Vector3 position)
        {
            _currentGrenadeLauncher.Launch(position);
        }

        public WeaponConfig GetWeaponInfoByType(WeaponType type)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].Config.WeaponType == type)
                {
                    return _weapons[i].Config;
                }
            }

            return null;
        }
        
        private void SetWeapon(int weaponID)
        {
            _currentWeapon = _weapons[weaponID];
            _nextWeapon = _weapons[GetNextWeaponID(weaponID)];
            
            OnCurrentWeaponChanged?.Invoke(_currentWeapon.Config);
        }

        private void SubscribeToThisWeapon(Weapon weapon)
        {
            weapon.OnReloadingRequired += CurrentWeaponReloadingRequired;
            weapon.OnFired += CurrentWeaponFired;
            weapon.OnAmmoChanged += CurrentWeaponAmmoChanged;
        }

        private void UnsubscribeFromThisWeapon(Weapon weapon)
        {
            weapon.OnReloadingRequired -= CurrentWeaponReloadingRequired;
            weapon.OnFired -= CurrentWeaponFired;
            weapon.OnAmmoChanged -= CurrentWeaponAmmoChanged;
        }

        private void CurrentWeaponReloadingRequired()
        {
            OnCurrentWeaponReloadingRequired?.Invoke();
        }

        private void CurrentWeaponFired()
        {
            OnCurrentWeaponFired?.Invoke();
        }

        private void CurrentWeaponAmmoChanged(int currentCount, int maxCount)
        {
            OnCurrentWeaponAmmoChanged?.Invoke(currentCount,maxCount);
        }
        
        private int GetNextWeaponID(int currentID)
        {
            currentID++;
            int nextID = currentID;
            if (nextID >= _weaponsCount) nextID = 0;
            return nextID;
        }
    }
}