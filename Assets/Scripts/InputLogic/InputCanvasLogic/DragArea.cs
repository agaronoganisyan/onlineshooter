using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace InputLogic.InputCanvasLogic
{
    public class DragArea : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [InputControl(layout = "Vector2")]
        [SerializeField] private string m_ControlPath;
        
        private Canvas _canvas;
        private RectTransform _baseRect;

        public float _sensitivity = 1.0f;
        private Vector2 lastTouchPosition;
        
        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
        
        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _baseRect = GetComponent<RectTransform>();

            
            _baseRect.sizeDelta = new Vector2((float)Screen.width/2, Screen.height) /_canvas.scaleFactor;
            _baseRect.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - lastTouchPosition;

            SendValueToControl(delta*_sensitivity);
            
            lastTouchPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        { 
            
        }
    }
}