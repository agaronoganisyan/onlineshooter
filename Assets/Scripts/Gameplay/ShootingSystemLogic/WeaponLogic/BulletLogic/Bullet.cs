using ConfigsLogic;
using Fusion;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic.TrailLogic;
using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.HitLogic;
using NetworkLogic.PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class Bullet : NetworkBehaviour, INetworkPoolable, IPredictedSpawnBehaviour
    {
        private ShootingSystemConfig _shootingSystemConfig;

        private TimerService _lifeTimer;

        private ITrail _sparkTrail;
        private ITrail _smokeTrail;

        public Transform Transform => _transform;
        private Transform _transform;

        private float _speed;
        [SerializeField] private float _length;
        
        public HitInfo Info => _info;
        [Networked] private HitInfo _info { get; set; }
        [Networked(OnChanged = nameof(OnFinished))] private NetworkBool _finished { get; set; }
        [Networked] private float _lifeTime { get; set; }
        [Networked] private int _fireTick { get; set; }
        [Networked] private Vector3 _firePosition { get; set; }
        [Networked] private Vector3 _fireVelocity { get; set; }
        [Networked] private Vector3 _movingDirection { get; set; }
        [Networked] private Vector3 _hitPosition { get; set; }
        
        public void InitNetworkState(HitInfo hitInfo, Vector3 startPosition, Vector3 direction, float speed, float lifeTime)
        {
            _info = hitInfo;
            _speed = speed;

            _finished = false;
            _fireTick = Runner.Tick;
            _firePosition = startPosition;
            _fireVelocity = direction * _speed;
            _movingDirection = direction;
            _lifeTime = lifeTime;
        }

        public override void Spawned()
        {
            StartLifeTimer(_lifeTime);
            
            if (IsProxy)
            {
                transform.position = _firePosition;
                transform.rotation = Quaternion.LookRotation(_fireVelocity);
                
                _smokeTrail.Show();
            }
            else _sparkTrail.Show();
            
            _finished = false; 
        }

        public override void FixedUpdateNetwork()
        {
            if (IsProxy) return;
            
            if (_finished) return;
            
            Vector3 previousPosition = GetMovePosition(Runner.Tick - 1);
            Vector3 nextPosition = GetMovePosition(Runner.Tick);
            Vector3 direction = (nextPosition - previousPosition).normalized;
            float distance = direction.magnitude;
            
            if (_length > 0f)
            {
                float elapsedDistanceSqr = (previousPosition - _firePosition).sqrMagnitude;
                float projectileLength = elapsedDistanceSqr > _length * _length ? _length : Mathf.Sqrt(elapsedDistanceSqr);

                previousPosition -= direction * projectileLength;
                distance += projectileLength;
            }
            
            HitOptions hitOptions = HitOptions.IncludePhysX | HitOptions.IgnoreInputAuthority;
            if (Runner.LagCompensation.Raycast(previousPosition, _movingDirection, distance,
                    Object.InputAuthority, out LagCompensatedHit hit, _shootingSystemConfig.ProjectileLayer, hitOptions))
            {
                IDamageable target = HitUtility.GetHitTarget(hit.Hitbox, hit.Collider);

                if (target == null)
                {
                    FinishMovement(hit.Point);
                    return;
                }

                if (!target.IsCanTakeHit(_info)) return;
                
                target.TakeDamage(this);
                FinishMovement(hit.Point);
            }
        }

        public override void Render()
        {
            if (_finished) return;
            
            float renderTime = IsProxy ? Runner.InterpolationRenderTime : Runner.SimulationRenderTime;
            float floatTick = renderTime / Runner.DeltaTime;

            _transform.position = GetMovePosition(floatTick);
        }
        
        private void StartLifeTimer(float duration)
        {
            _lifeTimer.Start(duration);
        }
        
        private void StopLifeTimer()
        {
            _lifeTimer.Stop();
        }

        private Vector3 GetMovePosition(float currentTick)
        {
            float time = (currentTick - _fireTick) * Runner.DeltaTime;

            if (time <= 0f)
                return _firePosition;

            return _firePosition + _fireVelocity * time;
        }

        private void FinishMovement(Vector3 position)
        {
            StopLifeTimer();
            _hitPosition = position;
            _finished = true;
        }

        #region POOL_LOGIC

        public void PoolInitialize()
        {
            _transform = transform;

            _shootingSystemConfig = ServiceLocator.Get<ShootingSystemConfig>();
            _lifeTimer = new StandardTimerService();

            _sparkTrail = _transform.GetChild(0).GetComponent<ITrail>();
            _smokeTrail = _transform.GetChild(1).GetComponent<ITrail>();
            
            _sparkTrail.Initialize();
            _smokeTrail.Initialize();
            
            _smokeTrail.OnFinished += ReturnToPool;
            _lifeTimer.OnFinished += () => FinishMovement(_transform.position);
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
            _sparkTrail.Hide();
            _smokeTrail.Hide();
            Runner.Despawn(Object);
        }
        
        public static void OnFinished(Changed<Bullet> changed)
        {
            changed.Behaviour.Finish();
        }

        private void Finish()
        {
            _transform.position = _hitPosition;
        }

        private Vector3 _interpolateFrom;
        private Vector3 _interpolateTo;
        
        public void PredictedSpawnSpawned()
        {
            _interpolateTo = transform.position;
            _interpolateFrom = _interpolateTo;
            transform.position = _interpolateTo;
            Spawned();
        }

        public void PredictedSpawnUpdate()
        {
            _interpolateFrom = _interpolateTo;
            _interpolateTo = transform.position;
            FixedUpdateNetwork();
        }

        void IPredictedSpawnBehaviour.PredictedSpawnRender() {
            var a = Runner.Simulation.StateAlpha;
            transform.position = Vector3.Lerp(_interpolateFrom, _interpolateTo, a);
        }

        public void PredictedSpawnFailed()
        {
            Runner.Despawn(Object, true);
        }

        public void PredictedSpawnSuccess()
        {
        }
    }
}