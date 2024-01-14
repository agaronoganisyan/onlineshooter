using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Infrastructure.LoadingCanvasLogic
{
    public class LoadingCanvas : MonoBehaviour, ILoadingCanvas
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private TimeSpan _fadingDuration;
        protected float _fadingAnimationDuration = 2.25f;
        
        public void Initialize()
        {
            _fadingDuration = TimeSpan.FromSeconds(_fadingAnimationDuration);
        }

        public async UniTask Show()
        {
            _canvasGroup.DOComplete();
            _canvas.enabled = true;
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _fadingAnimationDuration).SetEase(Ease.Linear);
            
            await UniTask.Delay(_fadingDuration); 
        }

        public void Hide()
        {
            _canvasGroup.DOComplete();
            _canvas.enabled = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0, _fadingAnimationDuration).SetEase(Ease.Linear).OnComplete(()=> _canvas.enabled = false);
        }
    }
}