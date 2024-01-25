using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvasSystem : CanvasSystemBase, IDebriefingCanvasSystem
    {
        private IMatchSystem _matchSystem;
        private IRespawnSystem _respawnSystem;
        
        public override void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();
            
            _matchSystem.OnFinished += () => Show();
            _respawnSystem.OnStarted += () => Show();
            _respawnSystem.OnFinished += () => Hide(true);
        }
    }
}