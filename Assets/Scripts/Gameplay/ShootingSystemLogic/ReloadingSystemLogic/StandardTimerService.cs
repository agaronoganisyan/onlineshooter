using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public class StandardTimerService : TimerService
    {
        protected override async UniTaskVoid Timer(TimeSpan duration, CancellationTokenSource cancellationToken)
        {
            TimerStarted();
            
            await UniTask.Delay(duration, cancellationToken: cancellationToken.Token);
            
            TimerFinished();
        }
    }
}