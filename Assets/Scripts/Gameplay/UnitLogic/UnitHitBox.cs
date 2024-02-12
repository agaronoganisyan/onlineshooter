using System;
using Gameplay.EffectsLogic;
using Gameplay.HealthLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public class UnitHitBox : MonoBehaviour, IUnitHitBox
    {
        public event Action<Vector3> OnHitTaken;

        private HealthSystem _healthSystem;
        private IEffectsFactory _effectsFactory;
        private Effect _hitEffect;
        
        private Transform _transform;

        private bool _isStopped;
        
        public void Initialize(HealthSystem healthSystem)
        {
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();
            
            _transform = transform;
            
            _healthSystem = healthSystem;
            _healthSystem.OnEnded += Stop;
        }
        
        public void Prepare()
        {
            _isStopped = false;
        }
        
        public void TakeDamage(Bullet bullet)
        {
            _hitEffect = _effectsFactory.GetHitEffect();
            _hitEffect.Play(bullet.Transform.position);
            
            BaseHealthDecrease(bullet.Damage, bullet.Transform);
        }

        public void TakeDamage(Grenade grenade)
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
            OnHitTaken?.Invoke(hittingTransform.forward);
        }

        private void Stop()
        {
            _isStopped = true;
        }
    }
}