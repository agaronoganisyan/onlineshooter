using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic
{
    public class InputCanvas : CanvasBase, IInputCanvas
    {
        private IInputCanvasSystem _canvasSystem;
        
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        public override void Initialize()
        {
            _canvasSystem = ServiceLocator.Get<IInputCanvasSystem>();
            
            _canvasSystem.OnShown += Show;
            _canvasSystem.OnHidden += Hide;
            
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
        }
    }
}