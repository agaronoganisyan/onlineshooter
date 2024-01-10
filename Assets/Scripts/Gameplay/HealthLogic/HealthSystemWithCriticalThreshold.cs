using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;

namespace Gameplay.HealthLogic
{
    public class HealthSystemWithCriticalThreshold : HealthSystem
    {
        public event Action OnBelowCriticalThreshold;
        public event Action OnAboveCriticalThreshold;
        
        private readonly ITimerService _timerForDelayBeforeRegeneration = new TimerService();
        private readonly ITimerService _regenerationTimer = new TimerService();
        
        private readonly float _criticalHealthThreshold;
        private readonly float _delayBeforeRegeneration;
        private readonly float _regenerationDegree;
        private readonly float _regenerationFrequency;
        
        private bool _isBelowCriticalThreshold;

        public HealthSystemWithCriticalThreshold(HealthSystemConfig config)
        {
            _criticalHealthThreshold = config.CriticalHealthThreshold;
            _delayBeforeRegeneration = config.DelayBeforeRegeneration;
            _regenerationDegree = config.RegenerationDegree;
            _regenerationFrequency = config.RegenerationFrequency;
            
            _timerForDelayBeforeRegeneration.OnFinished += StartRegeneration;
            _timerForDelayBeforeRegeneration.OnStopped += StopRegeneration;
            _regenerationTimer.OnFinished += StartRegeneration;
        }

        public override void Setup(float maxCount)
        {
            base.Setup(maxCount);
            _isBelowCriticalThreshold = false;
        }

        public override void Decrease(float count)
        {
            base.Decrease(count);

            StartRegenerationTimer();
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