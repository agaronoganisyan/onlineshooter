using System;
using Cysharp.Threading.Tasks;
using Infrastructure.CanvasBaseLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.LoadingCanvasLogic
{
    public class LoadingCanvas : CanvasBase, ILoadingCanvas
    {
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        private TimeSpan _fadingDuration;
        
        public override void Initialize()
        {
            _fadingDuration = TimeSpan.FromSeconds(_fadingAnimationDuration);
        }

        public async UniTask Show()
        {
            base.Show(true);
            _graphicRaycaster.enabled = true;
            await UniTask.Delay(_fadingDuration); 
        }

        public void Hide()
        {
            base.Hide(true);
            _graphicRaycaster.enabled = false;
        }
    }
}