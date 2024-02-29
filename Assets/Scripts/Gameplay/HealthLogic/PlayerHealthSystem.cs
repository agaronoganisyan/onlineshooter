using System;
using ConfigsLogic;
using Fusion;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.HealthLogic
{
    public class PlayerHealthSystem : HealthSystem
    {
        public event Action OnBelowCriticalThreshold;
        public event Action OnAboveCriticalThreshold;
        
        private HealthSystemConfig _config;
        
        private readonly TimerService _timerForDelayBeforeRegeneration = new StandardTimerService();
        private readonly TimerService _regenerationTimer = new StandardTimerService();
        
        private float _criticalHealthThreshold;
        private float _delayBeforeRegeneration;
        private float _regenerationDegree;
        private float _regenerationFrequency;
        
        private bool _isBelowCriticalThreshold;

        public void Initialize()
        {
            _config = ServiceLocator.Get<HealthSystemConfig>();

            _criticalHealthThreshold = _config.CriticalHealthThreshold;
            _delayBeforeRegeneration = _config.DelayBeforeRegeneration;
            _regenerationDegree = _config.RegenerationDegree;
            _regenerationFrequency = _config.RegenerationFrequency;
            
            _timerForDelayBeforeRegeneration.OnFinished += StartRegeneration;
            _timerForDelayBeforeRegeneration.OnStopped += StopRegeneration;
            _regenerationTimer.OnFinished += StartRegeneration;
        }

        public override void Prepare(float maxCount)
        {
            if (!HasStateAuthority) return;

            RPC_Prepare(maxCount);
        }

        public override void Decrease(float count)
        {
            if (!HasStateAuthority) return;

            RPC_Decrease(count);
        }

        public override void Increase(float count)
        {
            if (!HasStateAuthority) return;

            RPC_Increase(count);
        }

        private void StartRegenerationTimer()
        {
            _timerForDelayBeforeRegeneration.Stop();
            _timerForDelayBeforeRegeneration.Start(_delayBeforeRegeneration);
        }

        private void StartRegeneration()
        {
            Increase(_maxCount * _regenerationDegree);

            if (!IsHealthFull())_regenerationTimer.Start(_regenerationFrequency);
        }
        
        private void StopRegeneration()
        {
            _regenerationTimer.Stop();
        }
        
        protected override void HandleCurrentCount(float currentCount)
        {
            base.HandleCurrentCount(currentCount);
            
            if (currentCount == 0) return;
            
            if (_isBelowCriticalThreshold)
            {
                if (IsBelowCriticalThreshold(currentCount)) return;
                
                _isBelowCriticalThreshold = false;
                OnAboveCriticalThreshold?.Invoke();
            }
            else
            {
                if (!IsBelowCriticalThreshold(currentCount)) return;
                
                _isBelowCriticalThreshold = true;
                OnBelowCriticalThreshold?.Invoke();
            }
        }

        protected override void EndUp()
        {
            _timerForDelayBeforeRegeneration.Stop();
            _regenerationTimer.Stop();
            base.EndUp();
        }
        
        private bool IsBelowCriticalThreshold(float currentCount)
        {
            return currentCount / _maxCount <= _criticalHealthThreshold;
        }

        private bool IsHealthFull()
        {
            return _currentCount == _maxCount;
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Prepare(float maxCount)
        {
            _isBelowCriticalThreshold = false;
            base.Prepare(maxCount);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Decrease(float count)
        {
            StartRegenerationTimer();
            base.Decrease(count);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Increase(float count)
        {
            base.Increase(count);
        }
    }
}