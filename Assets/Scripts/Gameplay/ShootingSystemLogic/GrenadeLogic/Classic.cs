using System;
using Gameplay.UnitLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic
{
    public class Classic : Grenade
    {
        protected override void Explosion()
        {
            int hitTargetAmount = Physics.OverlapSphereNonAlloc(_transform.position, _impactRadius, _detectedColliders,_targetHitLayer);
            
            for (int i = 0; i < hitTargetAmount; i++)
            {
                _detectedColliders[i].TryGetComponent(out IDamageable target);
                target.TakeDamage(this);
            }
            
            int camerasAmount = Physics.OverlapSphereNonAlloc(_transform.position, _cameraShakeRadius, _detectedColliders,_cameraLayer);
            
            for (int i = 0; i < camerasAmount; i++)
            {
                _detectedColliders[i].TryGetComponent(out IShakable target);
                target.Shake();
            }
        }
    }
}