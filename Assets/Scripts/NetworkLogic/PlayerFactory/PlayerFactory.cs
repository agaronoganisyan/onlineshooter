using Cysharp.Threading.Tasks;
using Gameplay.MatchLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace NetworkLogic.PlayerFactory
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

        public async UniTask Create()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_playerAddress);
            Player obj = _networkManager.NetworkRunner.Spawn(prefab).GetComponent<Player>();
            _playerSystem.SetPlayer(obj);
            obj.SetInfo(_playerMatchInfo.Name, _playerMatchInfo.TeamType);
        }
    }
}