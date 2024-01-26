using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.CanvasBaseLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public interface ISharedGameplayCanvas : ICanvasBase
    {
        public void StartUpdating();
        public void StopUpdating();
    }
}