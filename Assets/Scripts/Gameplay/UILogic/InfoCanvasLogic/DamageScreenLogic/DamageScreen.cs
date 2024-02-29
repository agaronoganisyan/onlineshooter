using DG.Tweening;
using Gameplay.HealthLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.DamageScreenLogic
{
    public class DamageScreen : MonoBehaviour, IDamageScreen
    {
        private IPlayerSystem _playerSystem;
        
        [SerializeField] private CanvasGroup _screen;

        private Sequence _rippleAnimation;
        
        private float _effectAppearanceDuration = 0.5f;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _playerSystem.OnHealthBelowCriticalThreshold += StartEffect;
            _playerSystem.OnHealthAboveCriticalThreshold += () => StopEffect(true);
            _playerSystem.OnHealthEnded += () => StopEffect(false);

            _screen.alpha = 0;
        }

        private void StartEffect()
        {
            _screen.DOKill();
            _screen.alpha = 0;
            _screen.DOFade(1, _effectAppearanceDuration).OnComplete(StartRippleAnimation);
        }

        private void StopEffect(bool withAnimation)
        {
            _rippleAnimation.Kill();
            _screen.DOKill();
            if (withAnimation) _screen.DOFade(0, _effectAppearanceDuration);
            else _screen.alpha = 0;
        }
        
        
        private void StartRippleAnimation()
        {
            _rippleAnimation = DOTween.Sequence();
            _rippleAnimation.Append(_screen.DOFade(0.75f, 0.65f));//.SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(1f, 0.12f));//.SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(0.85f, 0.12f));//.SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(1f, 0.12f));//.SetEase(Ease.Linear);
            _rippleAnimation.SetLoops(-1);
        }
    }
}