using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public class TimerService : ITimerService
    {
        public event Action OnStarted;
        public event Action OnFinished;

        private CancellationTokenSource _cancellationTokenSource;
        
        public void Start(float duration)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Reloading(TimeSpan.FromSeconds(duration));
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
        
        private async UniTaskVoid Reloading(TimeSpan duration)
        {
            OnStarted?.Invoke();
            
            await UniTask.Delay(duration, cancellationToken: _cancellationTokenSource.Token);
            
            OnFinished?.Invoke();
        }
    }
}