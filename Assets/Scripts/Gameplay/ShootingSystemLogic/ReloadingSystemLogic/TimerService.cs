using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public abstract class TimerService 
    {
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action OnStopped;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public void Start(float duration)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Timer(TimeSpan.FromSeconds(duration), _cancellationTokenSource);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            OnStopped?.Invoke();
        }
        
        protected abstract UniTaskVoid Timer(TimeSpan duration, CancellationTokenSource cancellationToken);
        
        protected void TimerStarted() => OnStarted?.Invoke();
        protected void TimerFinished() => OnFinished?.Invoke();
    }
}