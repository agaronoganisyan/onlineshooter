using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.AssetManagementLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace NetworkLogic.MatchLogic
{
    public class NetworkMatchHandlerFactory : INetworkMatchHandlerFactory
    {
        private INetworkManager _networkManager;
        private IAssetsProvider _assetsProvider;
        
        private readonly TimeSpan _playersWaitingFrequency = TimeSpan.FromSeconds(1);
        
        private string _handlerAddress = "NetworkMatchHandler";

        private bool _isHandlerRegistered;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();

            _networkManager.OnPlayerJoinedRoom += (player) => PlayerJoinedRoom();
        }

        public async UniTask Register()
        {
            _isHandlerRegistered = false;
            
            while (!_isHandlerRegistered)
            {
                await UniTask.Delay(_playersWaitingFrequency);
            }
        }

        private async UniTask PlayerJoinedRoom()
        {
            if (_isHandlerRegistered) return;
            
            if (_networkManager.IsServerOrMasterClient())
            {
                GameObject prefab = await _assetsProvider.Load<GameObject>(_handlerAddress);
                INetworkMatchHandler obj = _networkManager.NetworkRunner.Spawn(prefab).GetComponent<INetworkMatchHandler>();
                obj.AddMasterClient(_networkManager.NetworkRunner.LocalPlayer);
                Register(obj);

                Debug.LogError("CREATE");
            }
            else
            {
                Register(NetworkMatchHandler.Instance);
                
                Debug.LogError("GET");
            }
        }

        private void Register(INetworkMatchHandler networkMatchHandler)
        {
            ServiceLocator.Register<INetworkMatchHandler>(networkMatchHandler);
            _isHandlerRegistered = true;
        }
    }
}