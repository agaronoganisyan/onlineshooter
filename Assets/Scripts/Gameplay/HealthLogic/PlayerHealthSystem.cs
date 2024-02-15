using System;
using ConfigsLogic;
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
            base.Prepare(maxCount);
            _isBelowCriticalThreshold = false;
        }

        public override void Decrease(float count)
        {
            StartRegenerationTimer();
            base.Decrease(count);
        }

        public override void Increase(float count)
        {
            base.Increase(count);
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
    }
}