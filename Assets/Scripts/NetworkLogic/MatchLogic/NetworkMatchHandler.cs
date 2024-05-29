using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using Gameplay.MatchLogic.TeamsLogic;
using NetworkLogic.MatchPointsLogic;
using NetworkLogic.PlayerNetworkObjectsDataLogic;
using NetworkLogic.TeamsSystemLogic;
using NetworkLogic.TimerLogic;
using UnityEngine;
using ResultFormat = NetworkLogic.TimerLogic.ResultFormat;

namespace NetworkLogic.MatchLogic
{
    public class NetworkMatchHandler : NetworkBehaviour, INetworkMatchHandler, INetworkRunnerCallbacks
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

        //private INetworkManager _networkManager;
        [Networked][Capacity(10)] private NetworkDictionary<PlayerRef, PlayerNetworkObjectsData> _allPlayersObjects => default;

        private int _lastTime = -1;

        //private TimerServiceForDisplay _timerService;
        
        public override void Spawned()
        {
            if (Instance)
            {
                Runner.Despawn(Object);
                return;
            }
            else
            {
                Instance = this;
                //_timerService = new TimerServiceForDisplay(ResultFormat.MinutesAndSeconds);
                //_timerService.OnValueGiven += RPC_MatchTimeGiven;
                //_timerService.OnValueChanged += RPC_RestOfMatchTimeChanged;
                //_timerService.OnStarted += RPC_Started;
                //_timerService.OnFinished += RPC_Finished;
            }
            
            Runner.AddCallbacks(this);
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority) return;
            
            //Debug.Log(_allPlayers.Count);
            
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

        public void AddMasterClient(PlayerRef playerRef)
        {
            PlayerJoinedRoom(playerRef);
        }

        private void PlayerJoinedRoom(PlayerRef playerRef)
        {
            Debug.Log($"PlayerJoinedRoom called for playerRef: {playerRef}");
            
            NetworkTeamsData.AddUnitToTeam(playerRef, new PlayerData());
            if (NetworkTeamsData.IsTeamsReady()) IsReady = true;
        }

        private void PlayerLeftRoom(PlayerRef playerRef)
        {
            if (!HasStateAuthority) return;
            
            DespawnLeftPlayerObjects(playerRef);
        }
        
        private void AddPlayerObject(PlayerRef player, NetworkId objId)
        {
            if (!HasStateAuthority) return;
            
            AddPlayerObjectToDictionary(player, objId);
        }

        private void DespawnLeftPlayerObjects(PlayerRef playerRef)
        {
            Debug.LogError("DespawnLeftPlayerObjects");
            
            if (_allPlayersObjects.ContainsKey(playerRef))
            {
                Debug.LogError($"DespawnLeftPlayerObjects ContainsKey Count {_allPlayersObjects[playerRef].Count()}");
                
                for (int i = 0; i < _allPlayersObjects[playerRef].Count(); i++)
                {
                    Debug.LogError("for");
                    DespawnNetworkObject(_allPlayersObjects[playerRef].GetObjectId(i));
                }

                _allPlayersObjects.Remove(playerRef);
            }
        }

        public void SetPlayerInfo(PlayerRef playerRef,string name)
        {
            Debug.LogError("SetPlayerInfo");
            PlayerData playerData = NetworkTeamsData.GetPlayerData(playerRef);
            playerData.SetBaseInfo(name);
            NetworkTeamsData.SetPlayerData(playerRef, playerData);
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_AddPlayerObject(PlayerRef player, NetworkId objId)
        {
            AddPlayerObject(player, objId);
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

        private async UniTask DespawnNetworkObject(NetworkId objId)
        {
            NetworkObject networkObject = Runner.FindObject(objId);
            
            if (networkObject == null) return;

            if (networkObject.HasStateAuthority)
            {
                Runner.Despawn(networkObject);
            }
            else if (Runner.IsSharedModeMasterClient)
            {
                networkObject.RequestStateAuthority();

                while (!networkObject.HasStateAuthority)
                {
                    await UniTask.Delay(100); // wait for Auth transfer
                }

                if (networkObject.HasStateAuthority)
                {
                    Runner.Despawn(networkObject);
                }
            }
        }

        void AddPlayerObjectToDictionary(PlayerRef player, NetworkId objId)
        {
            Debug.LogError($"AddPlayerObjectToDictionary player {player.PlayerId}");
            
            if (_allPlayersObjects.ContainsKey(player))
            {
                Debug.LogError("ContainsKey");

                PlayerNetworkObjectsData data = _allPlayersObjects[player];
                data.AddObject(objId);
                _allPlayersObjects.Set(player, data);
            }
            else
            {
                Debug.LogError("DON'T ContainsKey");

                PlayerNetworkObjectsData newData = new PlayerNetworkObjectsData();
                newData.AddObject(objId);
                _allPlayersObjects.Add(player, newData);
            }
        }

        #region NETWORK CALLBACKS

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.LogError("OnPlayerJoined");
            PlayerJoinedRoom(player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            PlayerLeftRoom(player);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        #endregion
    }
}