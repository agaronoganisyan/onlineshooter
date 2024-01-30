using System;
using System.Threading;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EnemiesDetectorLogic
{
    public class EnemiesDetector : IEnemiesDetector
    {
        public event Action<Transform> OnEnemyDetected;
        public event Action OnNoEnemyDetected;

        private readonly IEquipment _equipment;
        private readonly ShootingSystemConfig _shootingSystemConfig;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly TimeSpan _detectionRate;

        private readonly Collider[] _detectedColliders;
        private readonly LayerMask _targetHitLayer;
        private readonly LayerMask _obstacleLayer;
        
        private float _detectionZoneAngle;
        private float _detectionZoneRadius;
        float _minimalDistance = float.MaxValue;
        
        private readonly Transform _unit;
        private Transform _detectedEnemy;
        private Transform _previousEnemy;
        private Transform _finalEnemy;
        
        private readonly Vector3 _offsetForTargetShooting;
        
        public EnemiesDetector(ShootingSystemConfig shootingSystemConfig, Transform unit)
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            _equipment.OnCurrentWeaponChanged += WeaponChanged;
            
            _shootingSystemConfig = shootingSystemConfig;
            
            _detectedColliders = new Collider[_shootingSystemConfig.MaxDetectingCollidersAmount];
            _detectionRate = TimeSpan.FromSeconds(_shootingSystemConfig.DetectionFrequency);

            _targetHitLayer = _shootingSystemConfig.TargetHitLayer;
            _obstacleLayer = _shootingSystemConfig.ObstacleLayer;

            _offsetForTargetShooting = _shootingSystemConfig.OffsetForTargetShooting;

            _unit = unit;
        }

        private void WeaponChanged(Weapon weapon)
        {
            _detectionZoneAngle = weapon.WeaponConfig.DetectionZoneAngle;
            _detectionZoneRadius = weapon.WeaponConfig.DetectionZoneRadius;
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
                int collidersAmount = Physics.OverlapSphereNonAlloc(_unit.position, _detectionZoneRadius, _detectedColliders,_targetHitLayer);
                
                _finalEnemy = null;
                _detectedEnemy = null;
                _minimalDistance = float.MaxValue;
                    
                for (int i = 0; i < collidersAmount; i++)
                {
                    _detectedEnemy = _detectedColliders[i].transform;
                    if (DetectionFunctions.IsWithinAngle(_unit.position, _unit.forward, _detectedEnemy.position, _detectionZoneAngle) && !IsThereObstacle(_detectedEnemy.position))
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
        
        private bool IsThereObstacle(Vector3 targetPos)
        {
            return Physics.Linecast(_unit.position + _offsetForTargetShooting, targetPos + _offsetForTargetShooting,_obstacleLayer);
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