using System;
using DG.Tweening;
using HelpersLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public abstract class SharedGameplayCanvasObject: MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [SerializeField] private RectTransform _transform;
        private Transform _targetTransform;
        protected Transform _targetHeadTransform;

        private Vector3 _offsetToTarget;

        private float _fadingDuration = 0.5f;
        
        private bool _isEnabled;
        private bool _isHidden;
        
        public virtual void Show()
        {
            if (_isEnabled) return;
            
            _isEnabled = true;
            _canvasGroup.DOComplete();
            _canvasGroup.DOFade(1, _fadingDuration);
        }

        public virtual void Hide()
        {
            if (!_isEnabled) return;
            
            _isEnabled = false;
            _canvasGroup.DOComplete();
            _canvasGroup.DOFade(0, _fadingDuration);
        }
        
        protected void Enable()
        {
            gameObject.SetActive(true);
        }
        
        protected void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void SetParent(Transform parent)
        {
            _transform.SetParent(parent);
            _transform.localScale = Vector3.one;
        }

        public virtual void Tick()
        {
            if (_canvasGroup.alpha == 0) return;
            _transform.position = DetectionOnScreenFunctions.GetScreenPosition(_camera,_targetHeadTransform.position+ _offsetToTarget);
        }

        protected void Initialize(Transform target, Transform targetHead, Vector3 offsetToTarget, Camera worldCamera)
        {
            _targetTransform = target;
            _targetHeadTransform = targetHead;
            _offsetToTarget = offsetToTarget;
            _camera = worldCamera;
        }
    }
}