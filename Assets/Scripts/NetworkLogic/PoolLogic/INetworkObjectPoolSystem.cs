using Fusion;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.PoolLogic
{
    public interface INetworkObjectPoolSystem : INetworkObjectPool, IService
    {
        void Initialize();
        void ClearPools();
    }
}