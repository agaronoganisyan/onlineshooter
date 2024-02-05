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
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerHealthSystem = ServiceLocator.Get<PlayerHealthSystem>();
            _playerHealthSystem.OnBelowCriticalThreshold += StartEffect;
            _playerHealthSystem.OnAboveCriticalThreshold += StopEffect;

            _screen.alpha = 0;
            
            _rippleAnimation = DOTween.Sequence()
                .SetAutoKill(false)
                .Pause();
            _rippleAnimation.Append(_screen.DOFade(.5f, 0.8f)).SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(1f, .25f)).SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(.75f, .1f)).SetEase(Ease.Linear);
            _rippleAnimation.Append(_screen.DOFade(1f, .1f)).SetEase(Ease.Linear);
            _rippleAnimation.SetLoops(-1);
        }

        private void StartEffect()
        {
            _rippleAnimation.Pause();
            _screen.DOComplete();
            _screen.DOFade(1, .5f).OnComplete(() => { _rippleAnimation.Play(); });
        }

        private void StopEffect()
        {
            _rippleAnimation.Pause();
            _screen.DOComplete();
            _screen.DOFade(0, 1);
        }
    }
}