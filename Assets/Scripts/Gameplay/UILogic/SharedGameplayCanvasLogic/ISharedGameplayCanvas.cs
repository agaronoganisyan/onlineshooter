using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public interface ISharedGameplayCanvas
    {
        public void StartUpdating();
        public void Stop();
        void AddObject(IPlayerInfoBlock obj);
    }
}