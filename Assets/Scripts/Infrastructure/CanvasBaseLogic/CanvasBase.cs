using DG.Tweening;
using UnityEngine;

namespace Infrastructure.CanvasBaseLogic
{
    public abstract class CanvasBase : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected float _fadingAnimationDuration = 2.25f;
        
        public virtual void Initialize()
        {
            Hide();
        }
        
        public virtual void Show(bool withAnimation = false)
        {
            _canvasGroup.DOComplete();
            _canvas.enabled = true;
            _canvasGroup.alpha = 0;

            if (withAnimation) _canvasGroup.DOFade(1, _fadingAnimationDuration).SetEase(Ease.Linear);
            else _canvasGroup.alpha = 1;
        }

        public virtual void Hide(bool withAnimation = false)
        {
            _canvasGroup.DOComplete();
            _canvas.enabled = true;
            _canvasGroup.alpha = 1;

            if (withAnimation) _canvasGroup.DOFade(0, _fadingAnimationDuration).SetEase(Ease.Linear).OnComplete(()=> _canvas.enabled = false);
            else _canvas.enabled = false;
        }
    }
}