using DG.Tweening;
using UnityEngine;

namespace Infrastructure.CanvasPanelBase
{
    public class PanelBase  : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _fadingAnimationDuration = .5f;
        
        public virtual void Initialize()
        {
            Hide();
        }
        
        public virtual void Show(bool withAnimation = false)
        {
            if (gameObject.activeSelf) return;
            
            _canvasGroup.DOComplete();
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;

            if (withAnimation) _canvasGroup.DOFade(1, _fadingAnimationDuration).SetEase(Ease.Linear);
            else _canvasGroup.alpha = 1;
        }

        public virtual void Hide(bool withAnimation = false)
        {
            if (!gameObject.activeSelf) return;
            
            _canvasGroup.DOComplete();
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;

            if (withAnimation) _canvasGroup.DOFade(0, _fadingAnimationDuration).SetEase(Ease.Linear).
                OnComplete(() => gameObject.SetActive(false));
            else gameObject.SetActive(false);
        }
    }
}