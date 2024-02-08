using System;
using ConfigsLogic;
using Gameplay.EffectsLogic;
using Gameplay.UnitLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic
{
    public enum GrenadeType
    {
        None,
        Classic
    }
    
    public class Grenade : MonoBehaviour, IPoolable<Grenade>
    {
        private Action<Grenade> _returnToPool;

        private ShootingSystemConfig _shootingSystemConfig;
        private IEffectsFactory _effectsFactory;
        private Effect _hitEffect;
        
        [SerializeField] private Rigidbody _rigidbody;
        
        protected Collider[] _detectedColliders;
        
        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;

        public float Damage => _damage;
        private float _damage;
        protected float _impactRadius;

        public void Activate(Vector3 startPosition, Vector3 targetPosition, float damage, float impactRadius)
        {
            _damage = damage;
            _impactRadius = impactRadius;
            
            Vector3 direction = GetLaunchingDirection(startPosition,targetPosition, -_shootingSystemConfig.GrenadeLaunchingAngle).normalized;
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            gameObject.SetActive(true);
            
            _rigidbody.velocity = direction * BallisticFunctions.GetForce(startPosition,targetPosition, _shootingSystemConfig.GrenadeLaunchingAngle);
            Vector3 rotationDirection = new Vector3(10,20,0);
            _rigidbody.angularVelocity = rotationDirection;// TEST
        }
        
        private void OnCollisionEnter(Collision other)
        {
            Explosion();
            gameObject.SetActive(false);
        }

        private void Explosion()
        {
            Vector3 position = _transform.position;
            
            _hitEffect = _effectsFactory.GetGrenadeEffect();
            _hitEffect.Play(position);
            
            int hitTargetAmount = Physics.OverlapSphereNonAlloc(position, _impactRadius, _detectedColliders,_shootingSystemConfig.TargetHitLayer);
            
            for (int i = 0; i < hitTargetAmount; i++)
            {
                if (IsThereObstacle(_detectedColliders[i].transform.position)) return;
                if (!_detectedColliders[i].TryGetComponent(out IDamageable target)) return;
                target.TakeDamage(this);
            }
            
            int camerasAmount = Physics.OverlapSphereNonAlloc(position, _shootingSystemConfig.CameraDetectionRadius, _detectedColliders,_shootingSystemConfig.CameraLayer);
            
            for (int i = 0; i < camerasAmount; i++)
            {
                if (!_detectedColliders[i].TryGetComponent(out IShakable target)) return;
                target.Shake();
             target.Shake();
            }
        }
        
        private Vector3 GetLaunchingDirection(Vector3 startPosition, Vector3 targetPosition, float angle)
        {
            Vector3 directionWithoutY = Vector3Helper.GetDirectionWithoutY(startPosition, targetPosition);
            Quaternion directionRotation = Quaternion.LookRotation(directionWithoutY);
            directionRotation = Quaternion.Euler(angle, directionRotation.eulerAngles.y, directionRotation.eulerAngles.z);
            return directionRotation * Vector3.forward;
        }

        private bool IsThereObstacle(Vector3 targetPos)
        {
            return Physics.Linecast(_transform.position + _shootingSystemConfig.OffsetForTargetShooting, targetPos + _shootingSystemConfig.OffsetForTargetShooting,_shootingSystemConfig.ObstacleLayer);
        }
        
        private void OnDisable()
        {
            ReturnToPool();
        }

        #region POOL_LOGIC

        public void PoolInitialize(Action<Grenade> returnAction)
        {
            _returnToPool = returnAction;

            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();
            _detectedColliders = new Collider[_shootingSystemConfig.MaxDetectingCollidersAmount];
        }

        public void ReturnToPool()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity  = Vector3.zero;
            _returnToPool?.Invoke(this);
        }

        #endregion
    }
}