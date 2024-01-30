using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using HelpersLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic
{
   public enum WeaponType
    {
        None,
        First,
        Second
    }
    public enum WeaponModelType
    {
        None,
        Rifle,
        Pistol
    }
    
    public abstract class Weapon : MonoBehaviour
    {
        public event Action OnReloadingStarted;
        public event Action OnReloadingFinished;
        public event Action OnFired;
        public event Action<int, int> OnAmmoChanged;

         public WeaponConfig WeaponConfig => _weaponConfig;
         [SerializeField] WeaponConfig _weaponConfig;

         private IFactory<Bullet> _bulletsFactory;
         private TimerService _timerService = new StandardTimerService();

         private Bullet _currentBullet;

         public Transform ShootPoint => _shootPoint;
         [SerializeField] private Transform _shootPoint;
         [SerializeField] private Transform _transform;
         private Transform _activeContainer;
         private Transform _reserveContainer;

         private int _ammo;
         private int _maxAmmoCount;

         private float _nextShootTime;
         private float _frequency;
         private float _bulletDamage;
         private float _bulletSpeed;
         private float _reloadingDuration;

         public bool IsReloading => _isReloading;
         private bool _isReloading;


         public void Initialize(Transform activeContainer,Transform reserveContainer)
         {
             _activeContainer = activeContainer;
             _reserveContainer = reserveContainer;
             
             _bulletsFactory = new BulletFactory(_weaponConfig.BulletPrefab,_weaponConfig.MaxAmmoCount/3);

             _timerService.OnStarted += ReloadingStarted;
             _timerService.OnFinished += ReloadingFinished;

             _maxAmmoCount = _weaponConfig.MaxAmmoCount;
             _ammo = _maxAmmoCount;

             _frequency = _weaponConfig.Frequency;
             _bulletDamage = _weaponConfig.Damage;
             _bulletSpeed = _weaponConfig.BulletSpeed;
             _reloadingDuration = _weaponConfig.ReloadingDuration;

             _isReloading = false;
         }

         public bool IsReadyToFire(Transform target, float minAngleToStartingShooting)
         {
             return DetectionFunctions.IsWithinAngle(_shootPoint.position, _shootPoint.forward, target.position,
                 minAngleToStartingShooting);
         }

         public void Fire()
         {
             if (_isReloading) return; 
             if (Time.time < _nextShootTime) return; //ДОБАВИТЬ БЫ РАЗБРОССССССС
             
             _currentBullet = _bulletsFactory.Get();
             _currentBullet.Activate(_shootPoint.position, _shootPoint.forward,_bulletDamage,_bulletSpeed);

             _nextShootTime = Time.time + 1 * _frequency;
             _ammo--;
             OnFired?.Invoke();
             SetAmmo(_ammo);
             
             if (_ammo <= 0) StartReloading();
         }

         public bool IsRequiredReloading()
         {
             return _ammo <= 0 ? true : false;
         }

         public bool IsReloadingPossible()
         {
             return _ammo < _maxAmmoCount ? true : false;
         }

         public void StartReloading()
         {
             _timerService.Start(_reloadingDuration);
         }

         public void StopReloading()
         {
             if (!_isReloading) return; 
             
             _isReloading = false;
             _timerService.Stop();
         }

         public void Draw()
         {
             _transform.SetParent(_activeContainer);
             _transform.localPosition = _weaponConfig.PositionInHand;
             _transform.localEulerAngles = _weaponConfig.RotationInHand;
         }

         public void LayDown()
         {
             _transform.SetParent(_reserveContainer);
             _transform.localPosition = _weaponConfig.PositionInContainer;
             _transform.localEulerAngles = _weaponConfig.RotationInContainer;
         }

         void SetAmmo(int ammoCount)
         {
             _ammo = ammoCount;
             OnAmmoChanged?.Invoke(_ammo, _maxAmmoCount);
         }

         void ReloadingStarted()
         {
             _isReloading = true;
             OnReloadingStarted?.Invoke();
         }

         void ReloadingFinished()
         {
             _isReloading = false;
             SetAmmo(_maxAmmoCount);
             OnReloadingFinished?.Invoke();
         }
    }
}