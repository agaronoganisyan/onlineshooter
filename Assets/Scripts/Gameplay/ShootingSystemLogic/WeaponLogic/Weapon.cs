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

         public WeaponConfig WeaponConfig => _weaponConfig;
         [SerializeField] WeaponConfig _weaponConfig;

         private IFactory<Bullet> _bulletsFactory; 
         private ITimerService _timerService;

         private Bullet _currentBullet;
        
         public Transform ShootPoint => _shootPoint;
         [SerializeField] private Transform _shootPoint;
         [SerializeField] private Transform _transform;

         private int _ammo;
         private int _maxAmmoCount;
         
         private float _nextShootTime;
         private float _frequency;
         private float _bulletDamage;
         private float _bulletSpeed;
         private float _reloadingDuration;
         
         private bool _isReloading;

         public void Initialize(Transform activeContainer,Transform reserveContainer)
         {
             _timerService = new TimerService();
             _bulletsFactory = new BulletFactory(_weaponConfig.BulletPrefab,_weaponConfig.MaxAmmoCount/3);

             _timerService.OnStarted += ReloadingStarted;
             _timerService.OnFinished += ReloadingFinished;

             _maxAmmoCount = _weaponConfig.MaxAmmoCount;
             _ammo = _maxAmmoCount;

             _frequency = _weaponConfig.Frequency;
             _bulletDamage = _weaponConfig.Damage;
             _bulletSpeed = _weaponConfig.BulletSpeed;
             _reloadingDuration = _weaponConfig.ReloadingDuration;
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

             Debug.Log("FIRE");
             
             _currentBullet = _bulletsFactory.Get();
             _currentBullet.Activate(_shootPoint.position, _shootPoint.forward,_bulletDamage,_bulletSpeed);

             _nextShootTime = Time.time + 1 * _frequency;
             _ammo--;
             
             if (_ammo <= 0) StartReloading();
         }

         public bool IsRequiredReloading()
         {
             return _ammo <= 0 ? true : false;
         }

         public void StartReloading()
         {
             _timerService.Start(_reloadingDuration);
         }
         
         void ReloadingStarted()
         {
             _isReloading = true;
             OnReloadingStarted?.Invoke();
         }
         
         void ReloadingFinished()
         {
             _isReloading = false;
             _ammo = _maxAmmoCount;
             OnReloadingFinished?.Invoke();
         }
    }
}