using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
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
        
        public SharedGameplayCanvasObjectFactory(ISharedGameplayCanvas sharedGameplayCanvas, PlayerConfig playerConfig,
            PlayerInfoBlockConfig playerInfoBlockConfig, Camera worldCamera)
        {
            _sharedGameplayCanvas = sharedGameplayCanvas;
            
            _playerConfig = playerConfig;
            _playerInfoBlockConfig = playerInfoBlockConfig;

            _worldCamera = worldCamera;

            _playerBlockInfoFactory = new PlayerInfoBlockFactory(_playerInfoBlockConfig.Prefab, _playerInfoBlockConfig.InitialPoolSize);
        }

        public IPlayerInfoBlock GetPlayerBlockInfo(HealthSystem healthSystem, Transform target, Transform targetHead)
        {
            IPlayerInfoBlock playerInfoBlock = _playerBlockInfoFactory.Get().GetComponent<IPlayerInfoBlock>();
            playerInfoBlock.Initialize(_playerConfig, _playerInfoBlockConfig, healthSystem, target,targetHead, _worldCamera);
            _sharedGameplayCanvas.AddObject(playerInfoBlock);
            return playerInfoBlock;
        }
    }
}