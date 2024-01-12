using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public interface ISharedGameplayCanvasObjectFactory : IService
    {
        IPlayerInfoBlock GetPlayerBlockInfo(HealthSystem healthSystem, Transform target, Transform targetHead);
    }
}