using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.MatchLogic
{
    public interface INetworkMatchHandlerFactory : IService
    {
        void Initialize();
        UniTask Register();
    }
}