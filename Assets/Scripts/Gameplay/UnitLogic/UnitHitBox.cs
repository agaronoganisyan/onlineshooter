using System;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using Gameplay.EffectsLogic;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Gameplay.UnitLogic.DamageLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public class UnitHitBox : NetworkBehaviour, IUnitHitBox
    {
        public event Action<Vector3> OnHitTaken;

        private TeamType _unitTeamType;
        
        private HealthSystem _healthSystem;
        private Hitbox _hitbox;
        private IEffectsFactory _effectsFactory;
        private Effect _hitEffect;
        
        private Transform _transform;

        private bool _isStopped;
        
        public void Initialize(Unit unit)
        {
            _effectsFactory = ServiceLocator.Get<IEffectsFactory>();

            _hitbox = GetComponentInChildren<Hitbox>();
            _transform = transform;
            
            _unitTeamType = unit.Info.TeamType;

            //if (!HasStateAuthority) return;

            _healthSystem = unit.GetHealthSystem();
            _healthSystem.OnEnded += Stop;
        }

        public void Prepare()
        {
            _isStopped = false;
            _hitbox.enabled = true;
            _hitbox.gameObject.SetActive(true);
        }

        public bool IsCanTakeHit(HitInfo hitInfo)
        {
            return _unitTeamType != hitInfo.TeamType && !_isStopped;
        }

        public void TakeDamage(Bullet bullet)
        {
            RPC_TakeBulletDamage(bullet.Info.Damage, bullet.Transform.position, bullet.Transform.forward);
        }

        public void TakeDamage(Grenade grenade)
        {
            RPC_TakeGrenadeDamage(grenade.Info.Damage, grenade.Transform.position);
        }
        
        private void BaseHealthDecrease(float damageValue, Vector3 hitDirection)
        {
            if (_isStopped) return;
            
            _healthSystem.Decrease(damageValue);
            
            TakeHit(hitDirection);
        }

        private void TakeHit(Vector3 hitDirection)
        {
            OnHitTaken?.Invoke(hitDirection);
        }

        private void Stop()
        {
            _isStopped = true;
            _hitbox.enabled = false;
            _hitbox.gameObject.SetActive(false);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_TakeBulletDamage(float damageValue, Vector3 hitPosition, Vector3 hitDirection)
        {
            BaseHealthDecrease(damageValue, hitDirection);

            //if (!IsProxy) return;
            if (_isStopped) return;

            _hitEffect = _effectsFactory.GetHitEffect();
            Vector3 hitEffectPosition = _transform.position;
            hitEffectPosition.y = hitPosition.y;
            _hitEffect.Play(hitEffectPosition);
        }

        [Rpc(RpcSources.All, RpcTargets.All)] 
        private void RPC_TakeGrenadeDamage(float damageValue, Vector3 hitDirection)
        {
            BaseHealthDecrease(damageValue, hitDirection); 
            
        }
    }
}