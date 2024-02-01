using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.EquipmentFactoryLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;

namespace Gameplay.ShootingSystemLogic.EquipmentLogic.EquipmentSystemLogic
{
    public class EquipmentSystem : IEquipmentSystem
    {
        private string _firstWeaponAddress = "Weapon_First_Rifle";
        private string _secondWeaponAddress = "Weapon_Second_Pistol";
        private string _grenadeLauncherAddress = "GrenadeLauncher_Classic";

        private IEquipmentFactory _equipmentFactory;
        private IEquipment _equipment;
        private Weapon _firstWeapon;
        private Weapon _secondWeapon;
        private GrenadeLauncher _grenadeLauncher;
        
        public void Initialize()
        {
            _equipmentFactory = ServiceLocator.Get<IEquipmentFactory>();
            _equipment = ServiceLocator.Get<IEquipment>();
        }
        
        public async UniTask Prepare()
        {
            _firstWeapon = await _equipmentFactory.GetWeapon(_firstWeaponAddress);
            _secondWeapon = await _equipmentFactory.GetWeapon(_secondWeaponAddress);
            _grenadeLauncher = await _equipmentFactory.GetGrenadeLauncher(_grenadeLauncherAddress);
            
            _equipment.Initialize(_firstWeapon, _secondWeapon, _grenadeLauncher);
        }
        
        public void ResetEquipment()
        {
            _equipmentFactory.UnloadEquipment(_firstWeaponAddress);
            _equipmentFactory.UnloadEquipment(_secondWeaponAddress);
            _equipmentFactory.UnloadEquipment(_grenadeLauncherAddress);
        }
    }
}