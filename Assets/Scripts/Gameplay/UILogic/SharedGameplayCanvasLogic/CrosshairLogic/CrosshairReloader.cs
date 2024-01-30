using DG.Tweening;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.CrosshairLogic
{
    public class CrosshairReloader : MonoBehaviour
    {
        private Tween _reloadingAnimation;
        
        [SerializeField] private RectTransform _pointIcon;
        [SerializeField] private RectTransform _reloadingIcon;
        
        private Vector3 _rotationDirection = new Vector3(0, 0, 360);

        private float _rotationSpeed = 1.5f;

        public void Initialize()
        {
            _reloadingAnimation = _reloadingIcon.DOLocalRotate(_rotationDirection, _rotationSpeed, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .Pause();
            
            Stop();
        }
        
        public void Play()
        {
            _pointIcon.gameObject.SetActive(false);
            _reloadingIcon.gameObject.SetActive(true);
            _reloadingAnimation.Play();
        }

        public void Stop()
        {
            _reloadingIcon.gameObject.SetActive(false);
            _pointIcon.gameObject.SetActive(true);
            _reloadingAnimation.Pause();
        }
    }
}