using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace NetworkLogic
{
    public interface INetworkManager : IService
    {
        void Initialize();
        UniTask ConnectToGameRoom();
    }
}