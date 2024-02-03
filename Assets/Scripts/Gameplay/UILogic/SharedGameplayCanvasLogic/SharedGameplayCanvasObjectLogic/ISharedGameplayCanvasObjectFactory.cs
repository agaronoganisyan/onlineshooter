using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public interface ISharedGameplayCanvasObjectFactory : IService
    {
        void Initialize();
        IPlayerInfoBlock GetPlayerBlockInfo(Unit info);
        void ReturnAllObjectToPool();
    }
}