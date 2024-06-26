using System;
using ConfigsLogic;
using Fusion;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using WeaponConfig = ConfigsLogic.WeaponConfig;

namespace Gameplay.ShootingSystemLogic.AimLogic
{
    public class Aim : NetworkBehaviour, IAim
    {
        public event Action<Vector3> OnAimPositionChanged;
        
        private IEnemiesDetector _enemiesDetector;
        private IEquipment _equipment;
        private ShootingSystemConfig _shootingSystemConfig;

        public Transform Transform => _transform;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _aimBaseTransform;
        private Transform _target;

        private Vector3 _offsetForTargetShooting;
        private Vector3 _offsetFromBaseUnit;
        private Vector3 _offsetFromTarget;
        
        private float _aimingSpeed;

        public void Initialize(IEnemiesDetector enemiesDetector, IEquipment equipment)
        {
            _enemiesDetector = enemiesDetector;
            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            _equipment = equipment;
            
            _offsetForTargetShooting = _shootingSystemConfig.OffsetForTargetShooting;
            _aimingSpeed = _shootingSystemConfig.AimingSpeed;
            
            _enemiesDetector.OnEnemyDetected += AimToTarget;
            _enemiesDetector.OnNoEnemyDetected += AimToBasePosition;
            _equipment.OnCurrentWeaponChanged += SetBasePosition;

            SetBasePosition(_equipment.CurrentWeaponInfo);
            AimToBasePosition();
        }

        public void FixedTick()
        {
            _transform.position = Vector3.Lerp(_transform.position, _target.position + _offsetForTargetShooting,
                _aimingSpeed * Runner.DeltaTime);
            
            OnAimPositionChanged?.Invoke(_transform.position);
        }
        
        private void SetBasePosition(WeaponConfig weaponInfo)
        {
            _offsetFromBaseUnit = new Vector3(0,0,weaponInfo.DetectionZoneRadius);
            _aimBaseTransform.localPosition = _offsetFromBaseUnit;
        }
        
        private void AimToTarget(Transform target)
        {
            _target = target;
        }

        private void AimToBasePosition()
        {
            _target = _aimBaseTransform;
        }
    }
}