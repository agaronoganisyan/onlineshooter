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
        
        private IPlayerInfoBlockFactory _playerBlockInfoFactory;
        
        public void Initialize()
        {
            _playerMatchInfo = ServiceLocator.Get<IPlayerMatchInfo>();
            
            _playerBlockInfoFactory = ServiceLocator.Get<IPlayerInfoBlockFactory>();  
        }
        
        public IPlayerInfoBlock GetPlayerBlockInfo(Unit unit)
        {
            IPlayerInfoBlock playerInfoBlock = _playerBlockInfoFactory.Get();
            playerInfoBlock.Prepare(
                unit,
                _playerMatchInfo.TeamType == unit.Info.TeamType);
            
            return playerInfoBlock;
        }

        public void ReturnAllObjectToPool()
        {
            _playerBlockInfoFactory.ReturnAllObjectToPool();
        }
    }
}