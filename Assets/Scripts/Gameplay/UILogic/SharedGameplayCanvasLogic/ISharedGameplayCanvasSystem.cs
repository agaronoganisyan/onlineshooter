using System;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Gameplay.UnitLogic;
using Infrastructure.CanvasBaseLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public interface ISharedGameplayCanvasSystem : ICanvasSystemBase
    {
        event Action OnUpdatingStarted;
        event Action OnUpdatingStopped;
        event Action<Unit> OnUnitInfoAdded;
        event Action<IPlayerInfoBlock> OnUnitInfoRemoved;
        void AddUnitInfoObject(Unit info);
        void RemoveUnitInfoObject(IPlayerInfoBlock playerInfoBlock);
        void StartUpdating();
        void StopUpdating();
    }
}