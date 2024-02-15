using DG.Tweening;
using Gameplay.HealthLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.DamageScreenLogic
{
    public class DamageScreen : MonoBehaviour, IDamageScreen
    {
        private PlayerHealthSystem _playerHealthSystem;
        
        [SerializeField] private CanvasGroup _screen;

        private Sequence _rippleAnimation;
        
        private float _effectAppearanceDuration = 0.5f;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerHealthSystem = ServiceLocator.Get<PlayerHealthSystem>();
            _playerHealthSystem.OnBelowCriticalThreshold += StartEffect;
            _playerHealthSystem.OnAboveCriticalThreshold += () => StopEffect(true);
            _playerHealthSystem.OnEnded += () => StopEffect(false);

            _screen.alpha = 0;
        }

        private void StartEffect()
        {
            _rippleAnimation.Pause();
            _screen.DOComplete();
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