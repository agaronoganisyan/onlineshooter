using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace InputLogic.InputCanvasLogic.DragAreaLogic
{
    public class DragArea : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private IInputService _inputService;
        
        private Canvas _canvas;
        private RectTransform _baseRect;

        private Vector2 lastTouchPosition;
        private bool dragging;
        
        private void Start()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            
            _canvas = GetComponentInParent<Canvas>();
            _baseRect = GetComponent<RectTransform>();
            
            _baseRect.sizeDelta = new Vector2((float)Screen.width/2, Screen.height) /_canvas.scaleFactor;
            _baseRect.anchoredPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
            lastTouchPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragging)
            {
                Vector2 touchDelta = eventData.position - lastTouchPosition;

                SendValueToControl(touchDelta);
                
                lastTouchPosition = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            dragging = false;
        }

        private void SendValueToControl(Vector2 touchDelta)
        {
            _inputService.SetRotationDelta(touchDelta);
        }
    }
}