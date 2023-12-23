using System;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic
{
    [Serializable]
    public class Equipment : IEquipment
    {
        public event Action<WeaponType> OnCurrentWeaponChanged;

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
            inputService.OnReloadingInputReceived += ReloadCurrentWeapon;
        }
        
        public void SetUp(Weapon[] weapons, GrenadeLauncher grenade)
        {
            _weapons = weapons;
            _currentGrenadeLauncher = grenade;

            _currentWeaponID = 0;
            _weaponsCount = _weapons.Length;
            
            SetWeapon(_currentWeaponID);
        }

        public void SwitchWeapon()
        {
            _currentWeaponID = GetNextWeaponID(_currentWeaponID);
            SetWeapon(_currentWeaponID);
            
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
        
        private void SetWeapon(int weaponID)
        {
            _currentWeapon = _weapons[weaponID];
            _nextWeapon = _weapons[GetNextWeaponID(weaponID)];
        }

        private void ReloadCurrentWeapon()
        {
            _currentWeapon.StartReloading();
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