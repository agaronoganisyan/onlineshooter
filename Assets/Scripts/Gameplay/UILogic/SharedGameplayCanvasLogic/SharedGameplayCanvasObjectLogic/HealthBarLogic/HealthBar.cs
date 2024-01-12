using Gameplay.HealthLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic
{
    public class HealthBar : ProgressBarWithEffect, IHealthBar
    {
        private HealthSystem _healthSystem;
        
        public void Initialize(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            
            _healthSystem.OnChanged += UpdateHealthInfo;
            
            Prepare();
        }
        
        private void UpdateHealthInfo(float currentCount, float maxCount)
        {
            UpdateValue(currentCount, maxCount);
        }
    }
}