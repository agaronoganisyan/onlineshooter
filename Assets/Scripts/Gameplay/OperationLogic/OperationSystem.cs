using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagementLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.OperationLogic
{
    public class OperationSystem : IOperationSystem
    {
        private string _operationAddress = "OperationConfig_Deathmatch";
        
        private IAssetsProvider _assetsProvider;
        
        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async UniTask<OperationConfig> GetOperation()
        {
            return await _assetsProvider.Load<OperationConfig>(_operationAddress);
        }

        public void UnloadOperation()
        {
            _assetsProvider.Unload(_operationAddress);
        }
    }
}