using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Infrastructure.GameFactoryLogic
{
    public interface IGameInfrastructureFactory : IService
    {
        void Initialize();
        UniTask CreateAndRegisterInfrastructure();
    }
}