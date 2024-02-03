using Gameplay.HealthLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic
{
    public class HealthBar : ProgressBarWithEffect, IHealthBar
    {
        private HealthSystem _healthSystem;
        
        public void Prepare(HealthSystem healthSystem, Color mainBarColor, Color backgroundColor)
        {
            _healthSystem = healthSystem;
            
            _healthSystem.OnChanged += UpdateHealthInfo;
            
            Prepare(mainBarColor, backgroundColor);
        }

        public void Cleanup()
        {
            _healthSystem.OnChanged -= UpdateHealthInfo;
        }

        private void UpdateHealthInfo(float currentCount, float maxCount)
        {
            UpdateValue(currentCount, maxCount);
        }
    }
}