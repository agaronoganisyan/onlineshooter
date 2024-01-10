using DG.Tweening;
using Gameplay.HealthLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.DamageScreenLogic
{
    public class DamageScreen : MonoBehaviour, IDamageScreen
    {
        private HealthSystemWithCriticalThreshold _healthSystem;
        
        [SerializeField] private CanvasGroup _screen;

        private Sequence _rippleAnimation;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _healthSystem = ServiceLocator.Get<Player>().HealthSystem;
            _healthSystem.OnBelowCriticalThreshold += StartEffect;
            _healthSystem.OnAboveCriticalThreshold += StopEffect;

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

        private void RippleAnimation()
        {
            
        }
    }
}