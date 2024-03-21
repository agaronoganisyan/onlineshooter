using System;
using System.Threading;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EnemiesDetectorLogic
{
    public class EnemiesDetector : IEnemiesDetector
    {
        public event Action<Transform> OnEnemyDetected;
        public event Action OnNoEnemyDetected;

        private CancellationTokenSource _cancellationTokenSource ;

        private readonly IEquipment _equipment;
        private readonly ShootingSystemConfig _shootingSystemConfig;

        private readonly TimeSpan _detectionRate;

        private readonly Collider[] _detectedColliders;
        
        private float _detectionZoneAngle;
        private float _detectionZoneRadius;
        float _minimalDistance = float.MaxValue;

        public Transform Target => _previousEnemy;
        private Transform _previousEnemy;
        private readonly Transform _unit;
        private Transform _detectedEnemy;
        private Transform _finalEnemy;
        
        public EnemiesDetector(Transform unit, IEquipment equipment)
        {
            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();

            _equipment = equipment;
            _equipment.OnCurrentWeaponChanged += SetDetectionSettings;

            _detectedColliders = new Collider[_shootingSystemConfig.MaxDetectingCollidersAmount];
            _detectionRate = TimeSpan.FromSeconds(_shootingSystemConfig.DetectionFrequency);
            
            _unit = unit;

            SetDetectionSettings(_equipment.CurrentWeaponInfo);
        }

        private void SetDetectionSettings(WeaponConfig weaponInfo)
        {
            _detectionZoneAngle = weaponInfo.DetectionZoneAngle;
            _detectionZoneRadius = weaponInfo.DetectionZoneRadius; 
        }
        
        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Detection();
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        public bool IsThereTarget()
        {
            return _previousEnemy != null;
        }
        
        private async UniTask Detection()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                int collidersAmount = Physics.OverlapSphereNonAlloc(_unit.position, _detectionZoneRadius, _detectedColliders,_shootingSystemConfig.TargetHitLayer);
                
                _finalEnemy = null;
                _detectedEnemy = null;
                _minimalDistance = float.MaxValue;
                
                for (int i = 0; i < collidersAmount; i++)
                {
                    _detectedEnemy = _detectedColliders[i].transform;
                    if (!IsUnitItself(_detectedEnemy) 
                        && DetectionFunctions.IsWithinAngle(_unit.position, _unit.forward, _detectedEnemy.position, _detectionZoneAngle)
                        && !IsThereObstacle(_detectedEnemy.position))
                    {
                        float distance = GetDistanceToTarget(_detectedEnemy.position);
                            
                        if (distance <= _minimalDistance)
                        {
                            PrepareFinalEnemy(_detectedEnemy, distance);
                        }
                    } 
                }
                
                SetFinalTarget(_finalEnemy);
                
                await UniTask.Delay(_detectionRate, cancellationToken: _cancellationTokenSource.Token);
            }
        }
        
        private bool IsUnitItself(Transform detectedUnit)
        {
            return _unit == detectedUnit;
        }

        private bool IsThereObstacle(Vector3 targetPos)
        {
            return Physics.Linecast(_unit.position + _shootingSystemConfig.OffsetForTargetShooting, targetPos + _shootingSystemConfig.OffsetForTargetShooting,_shootingSystemConfig.ObstacleLayer);
        }
        
        private float GetDistanceToTarget(Vector3 targetPos)
        {
            return (targetPos - _unit.position).sqrMagnitude;
        }

        private void PrepareFinalEnemy(Transform finalEnemy, float distance)
        {
            _finalEnemy = finalEnemy;
            _minimalDistance = distance;
        }
        
        private void SetFinalTarget(Transform newTarget)
        {
            if (_previousEnemy == newTarget) return;

            _previousEnemy = newTarget;
               
            if (newTarget == null) OnNoEnemyDetected?.Invoke();
            else OnEnemyDetected?.Invoke(newTarget);
        }
    }
}
