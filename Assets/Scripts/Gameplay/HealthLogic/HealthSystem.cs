using System;
using Infrastructure.ServiceLogic;

namespace Gameplay.HealthLogic
{
    public abstract class HealthSystem : IService
    {
        public event Action<float, float> OnChanged;
        public event Action OnEnded;

        protected float _maxCount;
        protected float _currentCount;

        public virtual void Prepare(float maxCount)
        {
            _maxCount = maxCount;
            _currentCount = _maxCount;
            
            OnChanged?.Invoke(_currentCount, _maxCount);
        }

        public virtual void Decrease(float count)
        {
            _currentCount -= count;
            if (_currentCount <= 0) _currentCount = 0;

            HandleCurrentCount(_currentCount);
        }

        public virtual void Increase(float count)
        {
            _currentCount += count;
            if (_currentCount > _maxCount) _currentCount = _maxCount;

            HandleCurrentCount(_currentCount);
        }
        
        protected virtual void HandleCurrentCount(float currentCount)
        {
            OnChanged?.Invoke(currentCount, _maxCount);

            if (currentCount == 0)  EndUp();
        }
        
        protected virtual void EndUp()
        {
            OnEnded?.Invoke();
        }
    }
}