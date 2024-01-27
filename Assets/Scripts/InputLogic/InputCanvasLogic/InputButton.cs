using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace InputLogic.InputCanvasLogic
{
    public class InputButton : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [InputControl(layout = "Button")]
        [SerializeField] private string m_ControlPath;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;

        private float _disablingFadeValue = 0.2f;
        private float _fadingAnimationDuration = 0.15f;
        
        private float _scalingAnimationDuration = 0.15f;
        private float _scalingAnimationChangingValue = 0.65f;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        protected void Enable()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1, _fadingAnimationDuration);
        }
        
        protected void Disable()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(_disablingFadeValue, _fadingAnimationDuration);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_canvasGroup.interactable) return;
            _rectTransform.DOComplete();
            _rectTransform
                .DOScale(_rectTransform.localScale * _scalingAnimationChangingValue, _scalingAnimationDuration)
                .OnComplete(() => _rectTransform.DOScale(Vector3.one,_scalingAnimationDuration));
            SendValueToControl(1.0f);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
        }
    }
}