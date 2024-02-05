using System;
using DG.Tweening;
using Gameplay.UnitLogic;
using HelpersLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic
{
    public abstract class SharedGameplayCanvasObject: MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected Unit _unit;
        
        [SerializeField] private RectTransform _transform;

        private Vector3 _offsetToTarget;

        private float _fadingDuration = 0.5f;
        
        private bool _isShown;

        public virtual void Show()
        {
            if (_isShown) return;
            
            _isShown = true;
            _canvasGroup.DOComplete();
            _canvasGroup.DOFade(1, _fadingDuration);
        }

        public virtual void Hide()
        {
            if (!_isShown) return;
            
            _isShown = false;
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
            if (!gameObject.activeSelf) return;
            
            _transform.SetParent(parent);
            _transform.localScale = Vector3.one;
        }

        public virtual void Tick()
        {
            if (_canvasGroup.alpha == 0) return;
            _transform.position = DetectionOnScreenFunctions.GetScreenPosition(_camera,_unit.Info.HeadTransform.position + _offsetToTarget);
        }

        protected void Prepare(Unit unit, Vector3 offsetToTarget, Camera worldCamera)
        {
            _unit = unit;
            _offsetToTarget = offsetToTarget;
            _camera = worldCamera;
        }
    }
}