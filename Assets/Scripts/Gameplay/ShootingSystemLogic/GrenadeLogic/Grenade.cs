using System;
using ConfigsLogic;
using HelpersLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic
{
    public enum GrenadeType
    {
        None,
        Classic
    }
    
    public abstract class Grenade : MonoBehaviour, IPoolable<Grenade>
    {
        private Action<Grenade> _returnToPool;

        [SerializeField] private Rigidbody _rigidbody;
        
        protected Collider[] _detectedColliders;
        protected LayerMask _targetHitLayer;
        protected LayerMask _obstacleLayer;
        protected LayerMask _cameraLayer;
        
        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;

        public float Damage => _damage;
        private float _damage;
        
        protected float _impactRadius;
        protected float _cameraShakeRadius;

        private bool _isInitialized;
        
        public void PoolInitialize(Action<Grenade> returnAction)
        {
            _returnToPool = returnAction;
        }

        public void Activate(Vector3 startPosition, Vector3 targetPosition, ShootingSystemConfig shootingSystemConfig, GrenadeConfig grenadeConfig)
        {
            if (!_isInitialized) Initialize(shootingSystemConfig, grenadeConfig);

            Vector3 direction = GetLaunchingDirection(startPosition,targetPosition, -shootingSystemConfig.GrenadeLaunchingAngle).normalized;
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            gameObject.SetActive(true);
            
            _rigidbody.velocity = direction * BallisticFunctions.GetForce(startPosition,targetPosition, shootingSystemConfig.GrenadeLaunchingAngle);
            Vector3 rotationDirection = new Vector3(10,20,0);
            _rigidbody.angularVelocity = rotationDirection;// TEST
        }

        public void ReturnToPool()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity  = Vector3.zero;
            _returnToPool?.Invoke(this);
        }

        void Initialize(ShootingSystemConfig shootingSystemConfig, GrenadeConfig grenadeConfig)
        {
            _isInitialized = true;

            _detectedColliders = new Collider[shootingSystemConfig.MaxDetectingCollidersAmount];
            _targetHitLayer = shootingSystemConfig.TargetHitLayer;
            _obstacleLayer = shootingSystemConfig.ObstacleLayer;
            _cameraLayer= shootingSystemConfig.CameraLayer;
            
            _impactRadius = grenadeConfig.ImpactRadius;
            _cameraShakeRadius = shootingSystemConfig.CameraDetectionRadius;
            _damage = grenadeConfig.Damage;
        }

        private void OnCollisionEnter(Collision other)
        {
            Explosion();
        }

        protected abstract void Explosion();

        private void OnDisable()
        {
            ReturnToPool();
        }

        private Vector3 GetLaunchingDirection(Vector3 startPosition, Vector3 targetPosition, float angle)
        {
            Vector3 directionWithoutY = Vector3Helper.GetDirectionWithoutY(startPosition, targetPosition);
            Quaternion directionRotation = Quaternion.LookRotation(directionWithoutY);
            directionRotation = Quaternion.Euler(angle, directionRotation.eulerAngles.y, directionRotation.eulerAngles.z);
            return directionRotation * Vector3.forward;
        }
    }
}