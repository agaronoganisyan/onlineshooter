using System;
using Fusion;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.ShootingSystemLogic.ReloadingSystemLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchPointsLogic;
using NetworkLogic.TeamsSystemLogic;
using NetworkLogic.TimerLogic;
using UnityEngine;
using ResultFormat = NetworkLogic.TimerLogic.ResultFormat;

namespace NetworkLogic.MatchLogic
{
    public class NetworkMatchHandler : NetworkBehaviour, INetworkMatchHandler
    {
        public event Action OnStarted;
        public event Action OnFinished;
        public event Action<string> OnMatchTimeGiven;
        public event Action<string> OnRestOfMatchTimeChanged;
        public event Action<NetworkTeamsPointsData> OnTeamPointsChanged;
        
        public static NetworkMatchHandler Instance;
        [Networked] public ref NetworkTeamsData NetworkTeamsData => ref MakeRef<NetworkTeamsData>();
        [Networked] private ref NetworkTeamsPointsData NetworkTeamsPointsData => ref MakeRef<NetworkTeamsPointsData>();
        [Networked] public NetworkBool IsReady { get; private set; } = false;
        [Networked] public NetworkBool IsFinished { get; private set; } = false;
        [Networked] private NetworkTimer _timerServiceNew { get; set; }

        private INetworkManager _networkManager;

        private int _lastTime = -1;
        
        //private TimerServiceForDisplay _timerService;
        
        public override void Spawned()
        {
            if (Instance) Runner.Despawn(Object);
            else
            {
                Instance = this;
            
                _networkManager = ServiceLocator.Get<INetworkManager>();
                _networkManager.OnPlayerJoinedRoom += PlayerJoinedRoom; 
                
                //_timerService = new TimerServiceForDisplay(ResultFormat.MinutesAndSeconds);
                //_timerService.OnValueGiven += RPC_MatchTimeGiven;
                //_timerService.OnValueChanged += RPC_RestOfMatchTimeChanged;
                //_timerService.OnStarted += RPC_Started;
                //_timerService.OnFinished += RPC_Finished;
                
                PlayerJoinedRoom(_networkManager.NetworkRunner.LocalPlayer);
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority) return;
            
            if (IsFinished) return;
            
            if (_timerServiceNew.Expired(Runner)) RPC_Finished();

            int remainingTime = (int)_timerServiceNew.RemainingTime(Runner);
            
            if (_lastTime == remainingTime) return;
            
            RPC_RestOfMatchTimeChanged(_timerServiceNew.GetRemainingTimeInFormat(Runner,ResultFormat.MinutesAndSeconds));
            _lastTime = remainingTime;
        }

        public void IncreaseTeamPoints(TeamType teamType, int value)
        {
            NetworkTeamsPointsData.IncreaseTeamPoints(teamType, value);
            
            RPC_IncreaseTeamPoints(NetworkTeamsPointsData);
        }

        public void StartMatch(float duration)
        {
            _timerServiceNew = NetworkTimer.CreateFromSeconds(Runner, duration);
            OnMatchTimeGiven?.Invoke(_timerServiceNew.GetRemainingTimeInFormat(Runner, ResultFormat.MinutesAndSeconds));
            RPC_Started();
            //_timerService.Start(duration);
        }
        
        public TeamType GetWinningTeam()
        {
            return NetworkTeamsPointsData.GetWinningTeam();
        }
        
        private void PlayerJoinedRoom(PlayerRef playerRef)
        {
            NetworkTeamsData.AddUnitToTeam(playerRef, new PlayerData());
            if (NetworkTeamsData.IsTeamsReady()) IsReady = true;
        }

        //[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void SetPlayerInfo(PlayerRef playerRef, string name)
        {
            PlayerData playerData = NetworkTeamsData.GetPlayerData(playerRef);
            playerData.SetBaseInfo(name);
            NetworkTeamsData.SetPlayerData(playerRef, playerData);
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
            IsFinished = true;
            OnFinished?.Invoke();
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_IncreaseTeamPoints(NetworkTeamsPointsData networkTeamsPointsData)
        {
            OnTeamPointsChanged?.Invoke(networkTeamsPointsData);
        }
    }
}