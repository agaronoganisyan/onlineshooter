using Joystick_Pack.Scripts.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick_Pack.Scripts.Joysticks
{
    public class DynamicJoystick : Joystick
    {
        public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

        [SerializeField] private float moveThreshold = 1;

        protected override void Start()
        {
            MoveThreshold = moveThreshold;
            base.Start();
            baseRect.sizeDelta = new Vector2((float)Screen.width/2, Screen.height);
            baseRect.anchoredPosition = Vector2.zero;
            //background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            // background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.anchoredPosition = startPos;
            base.OnPointerUp(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > moveThreshold)
            {
                Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }
            base.HandleInput(magnitude, normalised, radius, cam);
        }
    }
}