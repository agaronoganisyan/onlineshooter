using Cysharp.Threading.Tasks;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.PlayerFactory
{
    public interface IPlayerFactory : IService
    {
        void Initialize();
        UniTask<Player> Create();
        
    }
}