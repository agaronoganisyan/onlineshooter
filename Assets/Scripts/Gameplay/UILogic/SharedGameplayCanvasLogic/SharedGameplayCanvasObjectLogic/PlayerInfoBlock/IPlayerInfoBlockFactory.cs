using Infrastructure.ServiceLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public interface IPlayerInfoBlockFactory : IService
    {
        void Initialize();
        PlayerInfoBlock Get();
        void ReturnAllObjectToPool();
    }
}