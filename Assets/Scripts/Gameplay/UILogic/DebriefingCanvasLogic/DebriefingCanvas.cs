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
        private IDebriefingCanvasSystem _canvasSystem;
        
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        [SerializeField] private DebriefingPanel _debriefingPanel;
        [SerializeField] private RespawnPanel _respawnPanel;
        
        public override void Initialize()
        {
            _canvasSystem = ServiceLocator.Get<IDebriefingCanvasSystem>();
            
            _canvasSystem.OnShown += Show;
            _canvasSystem.OnHidden += Hide;
            
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