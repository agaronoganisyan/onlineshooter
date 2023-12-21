using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public class TimerService : ITimerService
    {
        public event Action OnStarted;
        public event Action OnFinished;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        public void Start(float duration)
        {
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