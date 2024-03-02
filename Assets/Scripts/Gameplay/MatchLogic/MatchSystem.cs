using System;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;
using NetworkLogic;
using NetworkLogic.MatchLogic;

namespace Gameplay.MatchLogic
{
    public enum MatchResultType
    {
        None,
        Victory,
        Defeat,
        Draw
    }
    
    public class MatchSystem : IMatchSystem
    {
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action<string> OnMatchTimeGiven;
        public event Action<string> OnRestOfMatchTimeChanged;
        
        private INetworkManager _networkManager;
        private INetworkMatchHandler _networkMatchHandler;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
        }

        public void Prepare()
        {
            _networkMatchHandler = ServiceLocator.Get<INetworkMatchHandler>();
            _networkMatchHandler.OnMatchTimeGiven += (value) => OnMatchTimeGiven?.Invoke(value);
            _networkMatchHandler.OnRestOfMatchTimeChanged += (value) => OnRestOfMatchTimeChanged?.Invoke(value);
            _networkMatchHandler.OnStarted += () => OnStarted?.Invoke();
            _networkMatchHandler.OnFinished += () => OnFinished?.Invoke();
        }
        
        public async UniTask WaitingPlayers()
        {
            while (!_networkMatchHandler.IsReady)
            {
                await UniTask.Delay(1);
            }
        }
        
        public void Start(float duration)
        {
            if (!_networkManager.IsServerOrMasterClient()) return;
            
            _networkMatchHandler.StartMatch(duration);
        }

        public void Cleanup()
        {
        }
    }
}