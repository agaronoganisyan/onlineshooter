using System;
using ConfigsLogic;
using Fusion;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.DamageLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
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
    
    public abstract class Weapon : NetworkBehaviour
    {
         public event Action OnFired;
         public event Action<int, int> OnAmmoChanged;
         public event Action OnReloadingRequired;

         private Unit _unit;
         
         public WeaponConfig Config => _config;
         [SerializeField] WeaponConfig _config;

         private IBulletFactory _bulletsFactory;
         private Bullet _currentBullet;

         [SerializeField] private ParticleSystem _muzzleEffect;
         
         [SerializeField] private Transform _shootPoint; 
         [SerializeField] private Transform _transform;
         private Transform _activeContainer;
         private Transform _reserveContainer;

         private int _ammo;
         private int _maxAmmoCount;
         
         private float _nextShootTime;

         private bool _isReloading;
         
         public void Initialize(Unit unit, IEquipmentContainer equipmentContainer)
         {
             _unit = unit;
             
             _activeContainer = equipmentContainer.RightHandContainer;
             _reserveContainer = Config.WeaponType == WeaponType.First ?
                 equipmentContainer.FirstWeaponContainer :
                 equipmentContainer.SecondWeaponContainer;

             _bulletsFactory = ServiceLocator.Get<IBulletFactory>();

             _maxAmmoCount = _config.MaxAmmoCount;
             _ammo = _maxAmmoCount;

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
             
             _currentBullet = _bulletsFactory.Get(new HitInfo(_unit.Info.TeamType, _config.Damage),
                 _shootPoint.position, _shootPoint.forward, _config.BulletSpeed, _config.BulletLifetime);

             _nextShootTime = Time.time + 1 * _config.Frequency;
             _ammo--;
             RPC_PlayEffect();
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

         public void Draw()
         {
             _transform.SetParent(_activeContainer);
             _transform.localPosition = _config.PositionInHand;
             _transform.localEulerAngles = _config.RotationInHand;
         }

         public void LayDown()
         {
             _transform.SetParent(_reserveContainer);
             _transform.localPosition = _config.PositionInContainer;
             _transform.localEulerAngles = _config.RotationInContainer;
         }

         void SetAmmo(int ammoCount)
         {
             _ammo = ammoCount;
             OnAmmoChanged?.Invoke(_ammo, _maxAmmoCount);
         }

         void StartReloading()
         {
             _isReloading = true;
             OnReloadingRequired?.Invoke();
         }
         
         public void FinishReloading()
         {
             _isReloading = false;
             SetAmmo(_maxAmmoCount);
         }

         [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
         private void RPC_PlayEffect()
         {
             _muzzleEffect.Play();
         }
    }
}