using System;
using Fusion;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using NetworkLogic.PoolLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.EffectsLogic
{
    public class Effect : MonoBehaviour, INetworkPoolable, IPoolable<Effect>
    {
        private Action<Effect> _returnToPool;
        
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _transform;
        
        private TimerService _lifeTimer;
        
        //[Networked(OnChanged = nameof(OnShown))]
        private NetworkBool _shown { get; set; }
        //[Networked(OnChanged = nameof(OnDespawned))]
        private NetworkBool _despawned { get; set; }
        //[Networked]
        private Vector3 _playPosition { get; set; }
        private float _lifeTime = 1;

        public void Play(Vector3 position)
        {
            _playPosition = position;
            _shown = true;
            Show();
        }

        // public static void OnShown(Changed<Effect> changed)
        // {
        //     changed.Behaviour.Show();
        // }
        
        private void Show()
        {
            if (!_shown) return;
            
            _transform.position = _playPosition;
            gameObject.SetActive(true);
            _particleSystem.Play();
        }
        
        // public static void OnDespawned(Changed<Effect> changed)
        // {
        //     changed.Behaviour.Despawn();
        // }
        
        private void Despawn()
        {
            if (!_despawned) return;
            
            ReturnToPool();
        }
        
        private void OnParticleSystemStopped()
        {
            if (!_particleSystem.isStopped) return;
            _despawned = true;
            Despawn();
        }

        private void StartLifeTimer(float duration)
        {
            _lifeTimer.Start(duration);
        }
        
        private void StopLifeTimer()
        {
            _lifeTimer.Stop();
        }
        
        #region POOL_LOGIC

        public void PoolInitialize()
        {

        }

        public void PoolInitialize(Action<Effect> returnAction)
        {
            _returnToPool = returnAction; 

        }

        public void ReturnToPool()
        {
            _shown = false;
            _despawned = false;
            gameObject.SetActive(false);
            _returnToPool?.Invoke(this);
            //Runner.Despawn(Object);
        }
        
        #endregion
    }
}