using Infrastructure.ServiceLogic;

namespace NetworkLogic.ObjectFactoryLogic
{
    public interface INetworkFactoriesSystem : IService
    {
        void Initialize();
        void Prepare();
    }
}