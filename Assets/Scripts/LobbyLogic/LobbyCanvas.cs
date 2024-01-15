using Infrastructure.CanvasBaseLogic;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyLogic
{
    public class LobbyCanvas : CanvasBase, ILobbyCanvas
    {
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

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