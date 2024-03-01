using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic
{
    public interface INetworkManager : IService
    {
        public event Action<PlayerRef> OnPlayerJoinedRoom;
        public NetworkRunner NetworkRunner { get; }
        void Initialize();
        UniTask ConnectToGameRoom();
        bool IsServerOrMasterClient();
    }
}