using Cysharp.Threading.Tasks;
using Fusion;
using Gameplay.MatchLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic;
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
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
        }

        public async UniTask<Player> Create()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_playerAddress);
            Player player = _networkManager.NetworkRunner.Spawn(prefab, Vector3.zero, Quaternion.identity, _networkManager.NetworkRunner.LocalPlayer, 
                (runner, obj ) =>
                {
                    Player beforeSpawnPlayer = obj.GetComponent<Player>();
                    beforeSpawnPlayer.SetInfo(_playerMatchInfo.TeamType, _playerMatchInfo.Name);
                }).GetComponent<Player>();
            
            _playerSystem.SetPlayer(player);
            return player;
        }
    }
}