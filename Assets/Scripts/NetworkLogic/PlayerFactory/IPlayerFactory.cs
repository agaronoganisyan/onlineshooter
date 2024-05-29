using Cysharp.Threading.Tasks;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchLogic;

namespace NetworkLogic.PlayerFactory
{
    public interface IPlayerFactory : IService
    {
        void Initialize();
        UniTask Create(INetworkMatchHandler networkMatchHandler);
    }
}