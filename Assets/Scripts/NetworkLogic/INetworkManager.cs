using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic
{
    public interface INetworkManager : IService
    {
        public NetworkRunner NetworkRunner { get; }
        void Initialize();
        UniTask ConnectToGameRoom();
    }
}