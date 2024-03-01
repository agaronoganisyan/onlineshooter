using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.MatchLogic
{
    public interface INetworkMatchHandler : IService
    {
        [Networked] NetworkBool IsReady { get; }
    }
}