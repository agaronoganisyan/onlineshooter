using System;

namespace Gameplay.ShootingSystemLogic.ReloadingSystemLogic
{
    public interface ITimerService
    {
        event Action OnStarted;
        event Action OnFinished;
        event Action OnStopped;
        void Start(float duration);
        void Stop();
    }
}