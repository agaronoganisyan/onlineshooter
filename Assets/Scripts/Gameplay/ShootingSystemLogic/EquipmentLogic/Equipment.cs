using System;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    [Serializable]
    public class Equipment : IEquipment
    {
        public event Action OnCurrentWeaponReloadingStarted;
        public event Action OnCurrentWeaponReloadingFinished;
        public event Action OnWeaponSwitchingStarted;
        public event Action<WeaponType> OnCurrentWeaponChanged;
        public event Action OnGrenadeLaunchingStarted;

        public Weapon CurrentWeapon => _currentWeapon;
        private Weapon _currentWeapon;
        public Weapon NextWeapon => _nextWeapon;
        private Weapon _nextWeapon;
        
        private Weapon[] _weapons;
        
        public GrenadeLauncher CurrentGrenadeLauncher => _currentGrenadeLauncher;
        private GrenadeLauncher _currentGrenadeLauncher;

        private int _currentWeaponID;
        private int _weaponsCount;

        public Equipment(IInputService inputService)
        {
            inputService.OnSwitchingInputReceived += StartWeaponSwitch;
            inputService.OnReloadingInputReceived += ReloadCurrentWeapon;
            inputService.OnThrowingInputReceived += StartThrowing;
        }
        
        public void SetUp(Weapon[] weapons, GrenadeLauncher grenade)
        {
            _weapons = weapons;
            _currentGrenadeLauncher = grenade;

            _currentWeaponID = 0;
            _weaponsCount = _weapons.Length;
            
            SetWeapon(_currentWeaponID);
            SubscribeToThisWeapon(_currentWeapon);
        }

        public void SwitchWeapon()
        {
            UnsubscribeFromThisWeapon(_currentWeapon);
            
            _currentWeaponID = GetNextWeaponID(_currentWeaponID);
            SetWeapon(_currentWeaponID);
            SubscribeToThisWeapon(_currentWeapon);
            
            OnCurrentWeaponChanged?.Invoke(_currentWeapon.WeaponConfig.WeaponType);
        }

        public Weapon GetWeaponByType(WeaponType type)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].WeaponConfig.WeaponType == type)
                {
                    return _weapons[i];
                }
            }

            return null;
        }
        
        private void StartWeaponSwitch()
        {
            _currentWeapon.StopReloading();
            OnWeaponSwitchingStarted?.Invoke();
        }
        
        private void SetWeapon(int weaponID)
        {
            _currentWeapon = _weapons[weaponID];
            _nextWeapon = _weapons[GetNextWeaponID(weaponID)];
        }

        private void ReloadCurrentWeapon()
        {
            _currentWeapon.StartReloading();
        }
        
        private void StartThrowing()
        {
            OnGrenadeLaunchingStarted?.Invoke();
        }
        
        private void SubscribeToThisWeapon(Weapon weapon)
        {
            weapon.OnReloadingStarted += CurrentWeaponReloadingStarted;
            weapon.OnReloadingFinished += CurrentWeaponReloadingFinished;
        }

        private void UnsubscribeFromThisWeapon(Weapon weapon)
        {
            weapon.OnReloadingStarted -= CurrentWeaponReloadingStarted;
            weapon.OnReloadingFinished -= CurrentWeaponReloadingFinished;
        }
        void CurrentWeaponReloadingStarted()
        {
            OnCurrentWeaponReloadingStarted?.Invoke();
        }

        void CurrentWeaponReloadingFinished()
        {
            OnCurrentWeaponReloadingFinished?.Invoke();
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