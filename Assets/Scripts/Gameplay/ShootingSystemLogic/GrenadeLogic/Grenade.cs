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

        public void Activate(Vector3 startPosition, Vector3 targetPosition, GrenadeLaunchingConfig grenadeLaunchingConfig, GrenadeConfig grenadeConfig)
        {
            if (!_isInitialized) Initialize(grenadeLaunchingConfig, grenadeConfig);

            Vector3 direction = GetLaunchingDirection(startPosition,targetPosition, -grenadeLaunchingConfig.LaunchingAngle).normalized;
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            gameObject.SetActive(true);
            
            _rigidbody.velocity = direction * BallisticFunctions.GetForce(startPosition,targetPosition, grenadeLaunchingConfig.LaunchingAngle);
            Vector3 rotationDirection = new Vector3(10,20,0);
            _rigidbody.angularVelocity = rotationDirection;// TEST
        }

        public void ReturnToPool()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity  = Vector3.zero;
            _returnToPool?.Invoke(this);
        }

        void Initialize(GrenadeLaunchingConfig grenadeLaunchingConfig, GrenadeConfig grenadeConfig)
        {
            _isInitialized = true;

            _detectedColliders = new Collider[grenadeLaunchingConfig.MaxDetectingCollidersAmount];
            _targetHitLayer = grenadeLaunchingConfig.TargetHitLayer;
            _obstacleLayer = grenadeLaunchingConfig.ObstacleLayer;
            _cameraLayer= grenadeLaunchingConfig.CameraLayer;
            
            _impactRadius = grenadeConfig.ImpactRadius;
            _cameraShakeRadius = grenadeLaunchingConfig.CameraDetectionRadius;
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