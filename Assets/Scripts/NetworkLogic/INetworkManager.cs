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
        public event Action<PlayerRef> OnPlayerLeftRoom;
        public event Action OnLocalPlayerLeftRoom;
        event Action OnShutdowned;
        public NetworkRunner NetworkRunner { get; }
        void Initialize();
        UniTask ConnectToGameRoom();
        UniTask DisconnectFromRoom();
        bool IsServerOrMasterClient();
    }
}