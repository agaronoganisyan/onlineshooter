using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic.WeaponReloadingButtonLogic
{
    public class WeaponReloadingButton : InputButton
    {
        private IEquipment _equipment;
        private Weapon _weapon;

        [SerializeField] private Image _icon;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            
            _equipment.OnEquipmentChanged += Prepare;
            _equipment.OnCurrentWeaponChanged += ChangeWeapon;
            
            _equipment.OnCurrentWeaponReloadingStarted += Disable;
            _equipment.OnCurrentWeaponReloadingFinished += TryToEnableButton;
            _equipment.OnWeaponSwitchingStarted += Disable;
            _equipment.OnWeaponSwitchingFinished += TryToEnableButton;
            _equipment.OnGrenadeLaunchingStarted += Disable;
            _equipment.OnGrenadeLaunchingFinished += TryToEnableButton;
        }

        private void Prepare()
        {
            ChangeWeapon(_equipment.CurrentWeapon);
            TryToEnableButton();
        }

        private void AmmoChanged(int currentCount, int maxCount)
        {
            if (currentCount < maxCount) Enable();
            else Disable();
        }

        private void TryToEnableButton()
        {
            if(_weapon.IsReloadingPossible()) Enable();
            else Disable();
        }

        private void ChangeWeapon(Weapon weapon)
        {
            if (_weapon != null) _weapon.OnAmmoChanged -= AmmoChanged;

            _weapon = weapon;
            _weapon.OnAmmoChanged += AmmoChanged;
            
            SetIconSprite(_weapon.WeaponConfig.BulletIconSprite);
        }

        private void SetIconSprite(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}