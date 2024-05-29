using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic
{
    public interface IEquipmentSystem : IService
    {
        UniTask Prepare(INetworkMatchHandler networkMatchHandler);
        void Initialize();
        void ResetEquipment();
    }
}