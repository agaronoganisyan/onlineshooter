using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public enum ResultFormat
    {
        None,
        Seconds,
        MinutesAndSeconds
    }
    
    public class TimerServiceForDisplay : TimerService
    {
        public event Action<string> OnValueGiven;
        public event Action<string> OnValueChanged;

        private ResultFormat _resultFormat;
        
        private TimeSpan _currentTime;
        private readonly TimeSpan _valueUpdatingFrequency = TimeSpan.FromSeconds(1);

        public TimerServiceForDisplay(ResultFormat resultFormat)
        {
            _resultFormat = resultFormat;
        }

        protected override async UniTaskVoid Timer(TimeSpan duration, CancellationTokenSource cancellationToken)
        {
            _currentTime = duration;
            OnValueGiven?.Invoke(ResultInRightFormat(_currentTime));
            
            TimerStarted();

            while (_currentTime.TotalSeconds > 0 && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(_valueUpdatingFrequency, cancellationToken: cancellationToken.Token);
                
                _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
                OnValueChanged?.Invoke(ResultInRightFormat(_currentTime));
            }
            
            TimerFinished();
        }

        private string ResultInRightFormat(TimeSpan timeSpan)
        {
            if (_resultFormat == ResultFormat.MinutesAndSeconds) return $"{timeSpan:mm\\:ss}";
            else return ((int)timeSpan.TotalSeconds).ToString();
        }
    }
}