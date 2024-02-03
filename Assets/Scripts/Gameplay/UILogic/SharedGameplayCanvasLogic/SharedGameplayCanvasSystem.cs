using System;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Gameplay.UnitLogic;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public class SharedGameplayCanvasSystem : CanvasSystemBase, ISharedGameplayCanvasSystem
    {
        public event Action OnUpdatingStarted;
        public event Action OnUpdatingStopped;
        public event Action<Unit> OnUnitInfoAdded;
        public event Action<IPlayerInfoBlock> OnUnitInfoRemoved;
        
        private IMatchSystem _matchSystem;
        private IRespawnSystem _respawnSystem;

        public void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();

            _matchSystem.OnFinished += () => Hide(true);
            _respawnSystem.OnStarted += () => Hide(true);
            _respawnSystem.OnFinished += () => Show(true);
        }
        
        public void StartUpdating()
        {
            OnUpdatingStarted?.Invoke();
        }

        public void StopUpdating()
        {
            OnUpdatingStopped?.Invoke();
        }
        
        public void AddUnitInfoObject(Unit unit)
        {
            OnUnitInfoAdded?.Invoke(unit);
        }

        public void RemoveUnitInfoObject(IPlayerInfoBlock info)
        {
            OnUnitInfoRemoved?.Invoke(info);
        }
    }
}