using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Infrastructure.ServiceLogic;

namespace InputLogic.InputCanvasLogic.GrenadeLaunchingButtonLogic
{
    public class GrenadeLaunchingButton : InputButton
    {
        private IEquipment _equipment;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _equipment = ServiceLocator.Get<IEquipment>();

            _equipment.OnEquipmentChanged += Prepare;
            
            _equipment.OnCurrentWeaponReloadingStarted += Disable;
            _equipment.OnCurrentWeaponReloadingFinished += Enable;
            _equipment.OnWeaponSwitchingStarted += Disable;
            _equipment.OnWeaponSwitchingFinished += Enable;
            _equipment.OnGrenadeLaunchingStarted += Disable;
            _equipment.OnGrenadeLaunchingFinished += Enable;
        }

        private void Prepare()
        {
            Enable();
        }
    }
}