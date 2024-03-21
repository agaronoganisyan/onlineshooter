using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic.TrailLogic
{
    public class ParticleSystemTrail : MonoBehaviour, ITrail
    {
        public event Action OnFinished;

        private ParticleSystem _particleSystem;

        private CancellationTokenSource _cancellationTokenSource ;
        private readonly TimeSpan _trailHandlingRate = TimeSpan.FromSeconds(1);
        
        public void Initialize()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _particleSystem.Play();
            StartTrailHandler();
        }

        public void Hide()
        {
            _particleSystem.Stop();
            StopTrailHandler();
            gameObject.SetActive(false);
        }
        
        private void ParticleSystemFinished()
        {
            OnFinished?.Invoke();
        }

        private void StartTrailHandler()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            TrailHandler();
        }

        private void StopTrailHandler()
        {
            _cancellationTokenSource?.Cancel();
        }
        
        private async UniTask TrailHandler()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await UniTask.Delay(_trailHandlingRate, cancellationToken: _cancellationTokenSource.Token);
                
                if (_particleSystem.particleCount <= 0)
                {
                    ParticleSystemFinished();
                    StopTrailHandler();
                }
            }
        }
    }
}