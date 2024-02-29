using Gameplay.HealthLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.PlayerSystemLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.InfoCanvasLogic.HealthLogic
{
    public class HealthInfoBlock : FillableImage, IHealthInfoBlock
    {
        private IPlayerSystem _playerSystem;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerSystem = ServiceLocator.Get<IPlayerSystem>();
            _playerSystem.OnHealthChanged += UpdateHealthInfo;
            SetIconFill(1);
        }
        
        private void UpdateHealthInfo(float currentCount, float maxCount)
        {
            SetIconFill(currentCount / maxCount,true);
        }
    }
}