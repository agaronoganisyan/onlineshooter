using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.UILogic.InfoCanvasLogic.WeaponLogic;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.InfoCanvasLogic
{
    public class GameplayInfoCanvasSystem : CanvasSystemBase, IGameplayInfoCanvasSystem
    {
        private IMatchSystem _matchSystem;
        private IRespawnSystem _respawnSystem;

        public override void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();

            _matchSystem.OnFinished += () => Hide(true);
            _respawnSystem.OnStarted += () => Hide(true);
            _respawnSystem.OnFinished += () => Show(true);
        }
    }
}