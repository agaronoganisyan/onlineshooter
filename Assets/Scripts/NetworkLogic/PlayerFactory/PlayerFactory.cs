using Cysharp.Threading.Tasks;
using Fusion;
using Gameplay.MatchLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic;
using NetworkLogic.MatchLogic;
using NetworkLogic.PlayerFactory;
using UnityEngine;

namespace Infrastructure.GameFactoryLogic
{
    public class PlayerFactory : IPlayerFactory
    {
        private INetworkManager _networkManager;
        private IPlayerSystem _playerSystem;
        private IAssetsProvider _assetsProvider;
        private IPlayerMatchInfo _playerMatchInfo;
        
        private string _playerAddress = "NetworkPlayer";

        private Player _player;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
            
            _networkManager.OnLocalPlayerLeftRoom += Destroy;
        }

        public async UniTask Create(INetworkMatchHandler networkMatchHandler)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_playerAddress);
            _player = _networkManager.NetworkRunner.Spawn(prefab, Vector3.zero, Quaternion.identity, _networkManager.NetworkRunner.LocalPlayer, 
                (runner, obj ) =>
                {
                    Player beforeSpawnPlayer = obj.GetComponent<Player>();
                    beforeSpawnPlayer.SetInfo(_playerMatchInfo.TeamType, _playerMatchInfo.Name);
                }).GetComponent<Player>();
            
            networkMatchHandler.RPC_AddPlayerObject(_networkManager.NetworkRunner.LocalPlayer, _player.Object);
            
            _playerSystem.SetPlayer(_player);
        }

        private void Destroy()
        {
            Debug.LogError("DESTROY DESTROY");
            if (_player == null) return;
            Debug.LogError($"_player.Object.HasStateAuthority- {_player.Object.HasStateAuthority}");

            _networkManager.NetworkRunner.Despawn(_player.Object);
            //_player.Despawn(); раннер-то дохлый уже какой смысл от этого вызова надо чтобы NetworkMatchHandler управлял процессом удаления
            Debug.LogError("PLAYER DESTROYED");
        }
    }
}