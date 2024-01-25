using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyLogic
{
    public class LobbyCanvas : CanvasBase, ILobbyCanvas
    {
        private ILobbyCanvasSystem _canvasSystem;

        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        [SerializeField] private LobbyPanel _lobbyPanel;

        public override void Initialize()
        {
            _canvasSystem = ServiceLocator.Get<ILobbyCanvasSystem>();
            
            _canvasSystem.OnShown += Show;
            _canvasSystem.OnHidden += Hide;
            
            _lobbyPanel.Initialize();
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
        }
    }
}