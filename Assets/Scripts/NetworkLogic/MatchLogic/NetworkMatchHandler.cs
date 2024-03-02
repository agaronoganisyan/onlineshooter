using System;
using Fusion;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.MatchLogic
{
    public class NetworkMatchHandler : NetworkBehaviour, INetworkMatchHandler
    {
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action<string> OnMatchTimeGiven;
        public event Action<string> OnRestOfMatchTimeChanged;
        
        public static NetworkMatchHandler Instance;
        
        [Networked] public NetworkBool IsReady { get; private set; } = false;
        
        private INetworkManager _networkManager;
        private TimerServiceForDisplay _timerService;

        public override void Spawned()
        {
            if (Instance) Runner.Despawn(Object);
            else
            {
                Instance = this;
            
                _networkManager = ServiceLocator.Get<INetworkManager>();
                _networkManager.OnPlayerJoinedRoom += PlayerJoinedRoom; 

                _timerService = new TimerServiceForDisplay(ResultFormat.MinutesAndSeconds);
                _timerService.OnValueGiven += RPC_MatchTimeGiven;
                _timerService.OnValueChanged += RPC_RestOfMatchTimeChanged;
                _timerService.OnStarted += RPC_Started;
                _timerService.OnFinished += RPC_Finished;
                
                PlayerJoinedRoom(_networkManager.NetworkRunner.LocalPlayer);
            }
        }
        
        public void StartMatch(float duration)
        {
            _timerService.Start(duration);
        }
        
        private void PlayerJoinedRoom(PlayerRef playerRef)
        {
             IsReady = true;
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_MatchTimeGiven(string value)
        {
            OnMatchTimeGiven?.Invoke(value);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_RestOfMatchTimeChanged(string value)
        {
            OnRestOfMatchTimeChanged?.Invoke(value);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Started()
        {
            OnStarted?.Invoke();
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Finished()
        {
            OnFinished?.Invoke();
        }
    }
}