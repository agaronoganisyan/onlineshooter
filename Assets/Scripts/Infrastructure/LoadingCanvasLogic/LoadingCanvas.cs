using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.LoadingCanvasLogic
{
    public class LoadingCanvas : CanvasBase, ILoadingCanvas
    {
        private ILoadingScreenSystem _canvasSystem;
        
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        public override void Initialize()
        {
            _canvasSystem = ServiceLocator.Get<ILoadingScreenSystem>();
            
            _fadingAnimationDuration = ServiceLocator.Get<LoadingScreenSystemConfig>().ShowingDuration;
            
            _canvasSystem.OnShown += Show;
            _canvasSystem.OnHidden += Hide;
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