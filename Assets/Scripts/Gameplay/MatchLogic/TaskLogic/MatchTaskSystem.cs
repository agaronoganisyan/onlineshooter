using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Gameplay.OperationLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.MatchLogic.TaskLogic
{
    public class MatchTaskSystem : IMatchTaskSystem
    {
        public event Action<string> OnTaskChanged;

        private IOperationSystem _operationSystem;
        private OperationConfig _operationConfig;

        public void Initialize()
        {
            _operationSystem = ServiceLocator.Get<IOperationSystem>();
        }
        
        public async UniTask Prepare()
        {
            _operationConfig = await _operationSystem.GetOperation();
            SetTask(_operationConfig.Task);
        }

        public void Cleanup()
        {
            
        }

        private void SetTask(string task)
        {
            OnTaskChanged?.Invoke(task);
        }
    }
}