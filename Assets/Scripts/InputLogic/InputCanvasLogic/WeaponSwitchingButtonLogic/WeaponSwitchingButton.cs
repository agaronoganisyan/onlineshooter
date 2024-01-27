using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic.WeaponSwitchingButtonLogic
{
    public class WeaponSwitchingButton : InputButton
    {
        private IEquipment _equipment;
        
        [SerializeField] private Image _icon;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            
            _equipment.OnEquipmentChanged += Prepare;
            _equipment.OnCurrentWeaponChanged += (weapon) => ChangeWeapon();
            
            _equipment.OnCurrentWeaponReloadingStarted += Disable;
            _equipment.OnCurrentWeaponReloadingFinished += Enable;
            _equipment.OnWeaponSwitchingStarted += Disable;
            _equipment.OnWeaponSwitchingFinished += Enable;
            _equipment.OnGrenadeLaunchingStarted += Disable;
            _equipment.OnGrenadeLaunchingFinished += Enable;
        }

        private void Prepare()
        {
            ChangeWeapon();
            Enable();
        }
        
        private void ChangeWeapon()
        {
            SetIconSprite(_equipment.NextWeapon.WeaponConfig.WeaponIconSprite);
        }

        private void SetIconSprite(Sprite icon)
        {
            _icon.sprite = icon;
            _icon.SetNativeSize();
        }
    }
}