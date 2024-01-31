using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic.WeaponReloadingButtonLogic
{
    public class WeaponReloadingButton : InputButton
    {
        private Weapon _weapon;

        [SerializeField] private Image _icon;
        
        private void Start()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _equipment.OnCurrentWeaponChanged += ChangeWeapon;
            
            _inputHandler.OnReloadingInputStatusChanged += SetEnableStatus;
        }

        protected override void Prepare()
        {
            ChangeWeapon(_equipment.CurrentWeapon);
            TryToEnableButton();
        }

        protected override void SetEnableStatus(bool status)
        {
            if (status) TryToEnableButton();
            else Disable();
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