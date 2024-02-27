using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace NetworkLogic.PlayerFactory
{
    public interface IPlayerFactory : IService
    {
        void Initialize();
        UniTask Create();
    }
}