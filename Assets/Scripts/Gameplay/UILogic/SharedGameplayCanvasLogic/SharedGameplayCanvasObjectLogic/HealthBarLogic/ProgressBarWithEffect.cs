using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic
{
    public class ProgressBarWithEffect : MonoBehaviour
    {
        [SerializeField] private Slider _mainSlider;
        [SerializeField] private Slider _effectSlider;

        [SerializeField] private Image _mainSliderImage;
        [SerializeField] private Image _backgroundImage;
        
        private float _delayBeforeEffect = 0.25f;
        private float _effectDuration = 0.25f;

        protected void Prepare(Color mainSliderColor, Color backgroundColor)
        {
            _effectSlider.DOComplete();
            _mainSlider.value = 1;
            _effectSlider.value = 1;
            _mainSliderImage.color = mainSliderColor;
            _backgroundImage.color = backgroundColor;
        }

        protected void UpdateValue(float currentValue, float maxValue)
        {
            float targetValue = currentValue / maxValue;
            
            _mainSlider.value = targetValue;

            //_effectSlider.DOComplete();
            _effectSlider.DOValue(targetValue, _effectDuration).SetDelay(_delayBeforeEffect);
        }
    }
}