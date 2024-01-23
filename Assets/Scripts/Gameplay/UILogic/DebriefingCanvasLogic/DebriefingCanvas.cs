using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.UILogic.DebriefingCanvasLogic.DebriefingLogic;
using Gameplay.UILogic.DebriefingCanvasLogic.RespawnLogic;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : CanvasBase, IDebriefingCanvas
    {
        private IMatchSystem _matchSystem;
        private IRespawnSystem _respawnSystem;
        
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        [SerializeField] private DebriefingPanel _debriefingPanel;
        [SerializeField] private RespawnPanel _respawnPanel;
        
        public void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();
            
            _matchSystem.OnFinished += () => Show();
            _respawnSystem.OnStarted += () => Show();
            _respawnSystem.OnFinished += () => Hide(true);
            
            _debriefingPanel.Initialize();
            _respawnPanel.Initialize();
            
            base.Initialize();
        }
        
        public override void Show(bool withAnimation = false)
        {
            base.Show(withAnimation);
            _graphicRaycaster.enabled = true;
        }

        public override void Hide(bool withAnimation = false)
        {
            base.Hide(withAnimation);
            _graphicRaycaster.enabled = false;
            
            _debriefingPanel.Hide();
            _respawnPanel.Hide();
        }
    }
}