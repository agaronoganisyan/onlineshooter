using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic
{
    public interface IEquipmentSystem : IService
    {
        UniTask Prepare();
        void Initialize();
        void ResetEquipment();
    }
}