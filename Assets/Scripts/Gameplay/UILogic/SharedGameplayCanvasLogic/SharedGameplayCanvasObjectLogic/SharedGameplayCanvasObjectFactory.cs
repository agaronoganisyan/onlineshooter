using ConfigsLogic;
using Gameplay.CameraLogic;
using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public class SharedGameplayCanvasObjectFactory : ISharedGameplayCanvasObjectFactory
    {
        private IPlayerMatchInfo _playerMatchInfo;
        
        private PlayerInfoBlockConfig _playerInfoBlockConfig;

        private PlayerInfoBlockFactory _playerBlockInfoFactory;
        
        private Camera _gameplayCamera;

        public void Initialize()
        {
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
            
            _playerInfoBlockConfig = ServiceLocator.Get<PlayerInfoBlockConfig>();

            _gameplayCamera = ServiceLocator.Get<IGameplayCamera>().Camera;

            _playerBlockInfoFactory = new PlayerInfoBlockFactory(_playerInfoBlockConfig.Prefab, _playerInfoBlockConfig.InitialPoolSize);            
        }
        
        public IPlayerInfoBlock GetPlayerBlockInfo(UnitInfo info)
        {
            IPlayerInfoBlock playerInfoBlock = _playerBlockInfoFactory.Get().GetComponent<IPlayerInfoBlock>();
            playerInfoBlock.Initialize(
                info,
                _playerInfoBlockConfig,
                _gameplayCamera,
                _playerMatchInfo.TeamType == info.TeamType);
            return playerInfoBlock;
        }
    }
}