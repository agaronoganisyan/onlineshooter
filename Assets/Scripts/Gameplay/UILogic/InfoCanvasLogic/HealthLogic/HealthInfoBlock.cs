using Gameplay.HealthLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.InfoCanvasLogic.HealthLogic
{
    public class HealthInfoBlock : FillableImage, IHealthInfoBlock
    {
        private HealthSystem _healthSystem;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            // _healthSystem = ServiceLocator.Get<PlayerHealthSystem>();
            // _healthSystem.OnChanged += UpdateHealthInfo;
            SetIconFill(1);
        }
        
        private void UpdateHealthInfo(float currentCount, float maxCount)
        {
            SetIconFill(currentCount / maxCount,true);
        }
    }
}