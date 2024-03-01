using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.MatchLogic
{
    public class NetworkMatchHandler : NetworkBehaviour, INetworkMatchHandler
    {
        public static NetworkMatchHandler Instance;
        
        [Networked] public NetworkBool IsReady { get; private set; } = false;
        
        private INetworkManager _networkManager;
        
        public override void Spawned()
        {
            if (Instance) Runner.Despawn(Object);
            else
            {
                Instance = this;
            
                _networkManager = ServiceLocator.Get<INetworkManager>();
                _networkManager.OnPlayerJoinedRoom += PlayerJoinedRoom; 

                PlayerJoinedRoom(_networkManager.NetworkRunner.LocalPlayer);
            }
        }
        
        private void PlayerJoinedRoom(PlayerRef playerRef)
        {
             IsReady = true;
        }
    }
}