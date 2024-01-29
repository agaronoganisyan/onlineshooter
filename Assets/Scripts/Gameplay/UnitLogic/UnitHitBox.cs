using System;
using Gameplay.HealthLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using HelpersLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public class UnitHitBox : MonoBehaviour, IUnitHitBox
    {
        public event Action<Vector3> OnHitTaken;

        private HealthSystem _healthSystem;
        
        private Transform _transform;

        private bool _isStopped;
        
        public void Initialize(HealthSystem healthSystem)
        {
            _transform = transform;
            
            _healthSystem = healthSystem;
            _healthSystem.OnEnded += Stop;
        }
        
        public void Prepare()
        {
            _isStopped = false;
        }
        
        public void TakeDamage(RifleBullet bullet)
        {
            BaseHealthDecrease(bullet.Damage, bullet.Transform);
        }

        public void TakeDamage(Classic grenade)
        {
            BaseHealthDecrease(grenade.Damage, grenade.Transform);
        }
        
        private void BaseHealthDecrease(float damageValue, Transform hittingTransform)
        {
            if (_isStopped) return;
            
            _healthSystem.Decrease(damageValue);
            
            TakeHit(hittingTransform);
        }

        private void TakeHit(Transform hittingTransform)
        {
            OnHitTaken?.Invoke(Vector3Helper.GetDirectionWithoutYNormalized(_transform.position,hittingTransform.position));
        }

        private void Stop()
        {
            _isStopped = true;
        }
    }
}