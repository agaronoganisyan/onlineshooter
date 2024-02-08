using System;
using Gameplay.UnitLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private Action<Bullet> _returnToPool;

        [SerializeField] private TrailRenderer _trailRenderer;
        
        [SerializeField] private Rigidbody _rigidbody;

        public Transform Transform => _transform;
        [SerializeField] private Transform _transform;

        public float Damage => _damage;
        private float _damage;
        private float _speed;

        public void PoolInitialize(Action<Bullet> returnAction)
        {
            _returnToPool = returnAction;
        }

        //НАДО ПАРАМЕТРЫ СКОРОСТИ УРОНА И ТД ДОБАВИТЬ ПУЛЕ
        
        public void Activate(Vector3 startPosition, Vector3 direction, float speed, float damage)
        {
            _speed = speed;
            _damage = damage;
            
            _transform.SetPositionAndRotation(startPosition, Quaternion.LookRotation(direction));
            gameObject.SetActive(true);
            _trailRenderer.Clear();
            _trailRenderer.enabled = true;
            
            _rigidbody.velocity = _transform.forward * _speed;
        }

        public void ReturnToPool()
        {
            _trailRenderer.enabled = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _returnToPool?.Invoke(this);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(this);
                gameObject.SetActive(false);
            }
        }
        private void OnDisable()
        {
            ReturnToPool();
        }
    }
}