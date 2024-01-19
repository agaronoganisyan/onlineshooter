using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.OperationLogic;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic
{
    public enum TeamType
    {
        None,
        First,
        Second
    }
    
    public class MatchSystem : IMatchSystem
    {
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action<string> OnMatchTimeGiven;
        public event Action<string> OnRestOfMatchTimeChanged;
        
        private IOperationSystem _operationSystem;
        private TimerServiceForDisplay _timerService;
        private OperationConfig _operationConfig;
        
        public void Initialize()
        {
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
            _timerService = new TimerServiceForDisplay(ResultFormat.MinutesAndSeconds);
            _timerService.OnValueGiven += (value) => OnMatchTimeGiven?.Invoke(value);
            _timerService.OnValueChanged += (value) => OnRestOfMatchTimeChanged?.Invoke(value);
            _timerService.OnFinished += Finish;
        }

        public async UniTask Prepare()
        {
            _operationConfig = await _operationSystem.GetOperation();
        }
        
        public void Start()
        {
            _timerService.Start(_operationConfig.Duration);
            OnStarted?.Invoke();;
        }

        private void Finish()
        {
            OnFinished?.Invoke();
        }
        
        public void Cleanup()
        {

        }
    }
}