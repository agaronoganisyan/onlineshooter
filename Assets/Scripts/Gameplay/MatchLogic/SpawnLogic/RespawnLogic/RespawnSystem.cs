using System;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.MatchLogic.SpawnLogic.RespawnLogic
{
    public class RespawnSystem : IRespawnSystem
    {
        public event Action<string> OnRespawnTimeGiven;
        public event Action<string> OnRestOfRespawnTimeChanged;
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action OnStopped;

        private IPlayerSystem _playerSystem;
        private IMatchSystem _matchSystem;
        private TimerServiceForDisplay _timerService;

        private float _duration = 5;
        
        public void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _matchSystem.OnFinished += Stop;
            
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _playerSystem.OnDied += Start;
            
            _timerService = new TimerServiceForDisplay(ResultFormat.Seconds);
            _timerService.OnValueGiven += (value) => OnRespawnTimeGiven?.Invoke(value);
            _timerService.OnValueChanged += (value) => OnRestOfRespawnTimeChanged?.Invoke(value);
            _timerService.OnStarted += () => OnStarted?.Invoke();
            _timerService.OnFinished += () => OnFinished?.Invoke();;
            _timerService.OnStopped += () => OnStopped?.Invoke();
        }

        private void Start()
        {
            _timerService.Start(_duration);
        }
        
        private void Stop()
        {
            _timerService.Stop();
        }
    }
}