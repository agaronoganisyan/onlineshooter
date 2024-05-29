using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.EquipmentFactoryLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic.MatchLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic
{
    public class EquipmentSystem : IEquipmentSystem
    {
        private string _firstWeaponAddress = "Weapon_First_Rifle";
        private string _secondWeaponAddress = "Weapon_Second_Pistol";
        private string _grenadeLauncherAddress = "GrenadeLauncher_Classic";

        private IEquipmentFactory _equipmentFactory;
        private IEquipment _equipment;
        
        public void Initialize()
        {
            _equipmentFactory = ServiceLocator.Get<IEquipmentFactory>();
            _equipment = ServiceLocator.Get<IEquipment>();
        }
        
        public async UniTask Prepare(INetworkMatchHandler networkMatchHandler)
        {
            Weapon firstWeapon = await _equipmentFactory.GetWeapon(_firstWeaponAddress, networkMatchHandler);
            Weapon secondWeapon = await _equipmentFactory.GetWeapon(_secondWeaponAddress, networkMatchHandler);
            GrenadeLauncher grenadeLauncher = await _equipmentFactory.GetGrenadeLauncher(_grenadeLauncherAddress);
            
            _equipment.Initialize(firstWeapon, secondWeapon, grenadeLauncher);
        }
        
        public void ResetEquipment()
        {
            _equipmentFactory.UnloadEquipment(_firstWeaponAddress);
            _equipmentFactory.UnloadEquipment(_secondWeaponAddress);
            _equipmentFactory.UnloadEquipment(_grenadeLauncherAddress);
        }
    }
}