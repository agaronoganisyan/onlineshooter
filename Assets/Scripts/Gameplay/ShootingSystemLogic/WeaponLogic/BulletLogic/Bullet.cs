using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.UnitLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private Action<Bullet> _returnToPool;

        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _lifeTime;
        
        [SerializeField] private TrailRenderer _trailRenderer;
        
        [SerializeField] private Rigidbody _rigidbody;

        public Transform Transform => _transform;
        [SerializeField] private Transform _transform;

        public float Damage => _damage;
        private float _damage;
        private float _speed;
        
        public void Activate(Vector3 startPosition, Vector3 direction, float speed, float damage, float lifeTime)
        {
            _speed = speed;
            _damage = damage;
            
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            gameObject.SetActive(true);
            _trailRenderer.Clear();
            _trailRenderer.enabled = true;
            
            _rigidbody.velocity = _transform.forward * _speed;
            
            StartLifeTimer(lifeTime);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(this);
                gameObject.SetActive(false);
            }
        }
        
        private void StartLifeTimer(float duration)
        {
            _lifeTime = TimeSpan.FromSeconds(duration);
            _cancellationTokenSource = new CancellationTokenSource();
            LifeTimer();
        }
        
        private void StopLifeTimer()
        {
            _cancellationTokenSource?.Cancel();
        }
        
        private async UniTask LifeTimer()
        {
            await UniTask.Delay(_lifeTime, cancellationToken: _cancellationTokenSource.Token);
            gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            ReturnToPool();
        }
        
        #region POOL_LOGIC

        public void PoolInitialize(Action<Bullet> returnAction)
        {
            _returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            StopLifeTimer();
            gameObject.SetActive(false);
            _trailRenderer.enabled = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _returnToPool?.Invoke(this);
        }
        
        #endregion
    }
}