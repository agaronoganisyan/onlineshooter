using System.Collections.Generic;
using ConfigsLogic;
using Fusion;
using Gameplay.EffectsLogic;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Gameplay.UnitLogic.DamageLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.HitLogic;
using NetworkLogic.PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic
{
    public enum GrenadeType
    {
        None,
        Classic
    }

    public class Grenade : NetworkBehaviour, INetworkPoolable
    {
        private ShootingSystemConfig _shootingSystemConfig;
        private IEffectsFactory _effectsFactory;
        private Effect _hitEffect;

        private TimerService _lifeTimer;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        
        private Collider[] _detectedColliders;
        private List<LagCompensatedHit> _areaHits = new List<LagCompensatedHit>();

        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        
        [SerializeField] private GameObject _mesh;

        private float _impactRadius;
        private float _delayAfterExplosion = 1;
        
        public HitInfo Info => _info;
        [Networked] private HitInfo _info { get; set; }
        [Networked(OnChanged = nameof(OnFinished))] private NetworkBool _finished { get; set; }
        [Networked] private Vector3 _hitPosition { get; set; }

        public void InitNetworkState(HitInfo hitInfo, Vector3 startPosition, Vector3 targetPosition, float impactRadius)
        {
            _info = hitInfo;
            _impactRadius = impactRadius;

            _finished = false;

            Vector3 direction = GetLaunchingDirection(startPosition,targetPosition, -_shootingSystemConfig.GrenadeLaunchingAngle).normalized;
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            _collider.enabled = true;
            
            _rigidbody.velocity = direction * BallisticFunctions.GetForce(startPosition,targetPosition, _shootingSystemConfig.GrenadeLaunchingAngle);
            Vector3 rotationDirection = new Vector3(10,20,0);
            _rigidbody.angularVelocity = rotationDirection;
        }

        public override void Spawned()
        {
            _mesh.SetActive(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag(_shootingSystemConfig.ObstacleTag)) return;
            
            Explosion();             
        }

        private void Explosion()
        {
            if (_finished) return;
            
            if (IsProxy) return;
            
            FinishMovement();

            ApplyAreaDamage();
        }

        private void ShakeCameras()
        {
            int camerasAmount = Physics.OverlapSphereNonAlloc(_hitPosition, _shootingSystemConfig.CameraDetectionRadius, _detectedColliders,_shootingSystemConfig.CameraLayer);
            
            for (int i = 0; i < camerasAmount; i++)
            {
                if (!_detectedColliders[i].TryGetComponent(out IShakable target)) return;
                target.Shake();
            }
        }

        private void ApplyAreaDamage()
        {
            HitOptions hitOptions = HitOptions.IncludePhysX | HitOptions.IgnoreInputAuthority;
            int hitTargetAmount = Runner.LagCompensation.OverlapSphere(_hitPosition, _impactRadius, Object.InputAuthority, _areaHits,
                _shootingSystemConfig.TargetHitLayer, hitOptions);

            if (hitTargetAmount <= 0) return;
            
            for (int i = 0; i < hitTargetAmount; i++)
            {
                if (IsThereObstacle(_areaHits[i].GameObject.transform.position)) return;
                
                IDamageable target = HitUtility.GetHitTarget(_areaHits[i].Hitbox, _areaHits[i].Collider);

                if (target == null) return;
                
                target.TakeDamage(this);
            }
        }

        private void PlayExplosionEffect()
        {
            _mesh.SetActive(false);

            _hitEffect = _effectsFactory.GetGrenadeEffect();
            _hitEffect.Play(_hitPosition);
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

        private void FinishMovement()
        {
            _hitPosition = _transform.position;
            _finished = true;
            
            StartLifeTimer(_delayAfterExplosion);
        }
        
        public static void OnFinished(Changed<Grenade> changed)
        {
            changed.Behaviour.Finish();
        }
        
        private void Finish()
        {
            _transform.position = _hitPosition;
            PlayExplosionEffect();
            ShakeCameras();
        }
        
        #region POOL_LOGIC
        
        public void PoolInitialize()
        {
            //ВЫЗЫВАЕТСЯ НА ОБОИХ КЛИЕНТАХ
            
            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();
            _detectedColliders = new Collider[_shootingSystemConfig.MaxDetectingCollidersAmount];
            _lifeTimer = new StandardTimerService();
            
            _lifeTimer.OnFinished += ReturnToPool;
        }

        public void ReturnToPool()
        {
            RPC_ReturnToPool();
        }
        
        #endregion
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_ReturnToPool()
        {
            StopLifeTimer();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity  = Vector3.zero;
            _collider.enabled = false;
            Runner.Despawn(Object);
        }
        
        private void StartLifeTimer(float duration)
        {
            _lifeTimer.Start(duration);
        }
        
        private void StopLifeTimer()
        {
            _lifeTimer.Stop();
        }
    }
}