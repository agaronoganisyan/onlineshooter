using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.OperationLogic
{
    public interface IOperationSystem : IService
    {
        void Initialize();
        UniTask<OperationConfig> GetOperation();
        void UnloadOperation();
    }
}