using System;
using Gameplay.UnitLogic;
using Infrastructure.CanvasBaseLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public interface ISharedGameplayCanvasSystem : ICanvasSystemBase
    {
        event Action OnUpdatingStarted;
        event Action OnUpdatingStopped;
        event Action<UnitInfo> OnUnitInfoAdded;
        void AddObjectAddObject(UnitInfo info);
        void StartUpdating();
        void StopUpdating();
    }
}