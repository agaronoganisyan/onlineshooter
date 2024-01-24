using ConfigsLogic;
using Gameplay.CameraLogic;
using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public class SharedGameplayCanvasObjectFactory : ISharedGameplayCanvasObjectFactory
    {
        private PlayerConfig _playerConfig;
        private PlayerInfoBlockConfig _playerInfoBlockConfig;

        private ISharedGameplayCanvas _sharedGameplayCanvas;

        private PlayerInfoBlockFactory _playerBlockInfoFactory;

        private Camera _worldCamera;

        public void Initialize()
        {
            _playerConfig = ServiceLocator.Get<PlayerConfig>();
            _playerInfoBlockConfig = ServiceLocator.Get<PlayerInfoBlockConfig>();

            _worldCamera = ServiceLocator.Get<IGameplayCamera>().Camera;

            _playerBlockInfoFactory = new PlayerInfoBlockFactory(_playerInfoBlockConfig.Prefab, _playerInfoBlockConfig.InitialPoolSize);            
        }
        
        public IPlayerInfoBlock GetPlayerBlockInfo(HealthSystem healthSystem, Transform target, Transform targetHead)
        {
            IPlayerInfoBlock playerInfoBlock = _playerBlockInfoFactory.Get().GetComponent<IPlayerInfoBlock>();
            playerInfoBlock.Initialize(_playerConfig, _playerInfoBlockConfig, healthSystem, target,targetHead, _worldCamera);
            return playerInfoBlock;
        }
    }
}