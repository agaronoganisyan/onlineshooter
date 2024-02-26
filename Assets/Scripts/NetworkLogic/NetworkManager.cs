using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace NetworkLogic
{
    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Failed,
        Connected,
        Loading,
        Loaded
    }
    
    public class NetworkManager : MonoBehaviour, INetworkManager, INetworkRunnerCallbacks
    {
        private NetworkRunner _runner;

        private ConnectionStatus _status;

        public void Initialize()
        {
        }

        public async UniTask ConnectToGameRoom()
        {
            SetConnectionStatus(ConnectionStatus.Connecting, "");

            PrepareNetworkRunner();
            
            var result = await _runner.StartGame(new StartGameArgs() {
                GameMode = GameMode.Shared,
                SessionName = "Roommm",
            });

            if (result.Ok) {
                Debug.Log("УРА");
            } else {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            }
        }
        
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("OnPlayerJoined");
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("Connected to server");

            SetConnectionStatus(ConnectionStatus.Connected, "");
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

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log("OnShutdown");
            
            string message = "";
            switch (shutdownReason)
            {
                case ShutdownReason.IncompatibleConfiguration:
                    message = "This room already exist in a different game mode!";
                    break;
                case ShutdownReason.Ok:
                    message = "User terminated network session!"; 
                    break;
                case ShutdownReason.Error:
                    message = "Unknown network error!";
                    break;
                case ShutdownReason.ServerInRoom:
                    message = "There is already a server/host in this room";
                    break;
                case ShutdownReason.DisconnectedByPluginLogic:
                    message = "The Photon server plugin terminated the network session!";
                    break;
                default:
                    message = shutdownReason.ToString();
                    break;
            }
            SetConnectionStatus(ConnectionStatus.Disconnected, message);
            
            if(_runner!=null && _runner.gameObject)
                Destroy(_runner.gameObject);
        }
        
        private void SetConnectionStatus(ConnectionStatus status, string message)
        {
            _status = status;
        }

        private void PrepareNetworkRunner()
        {
            if(_runner == null) _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;
        }
    }
}