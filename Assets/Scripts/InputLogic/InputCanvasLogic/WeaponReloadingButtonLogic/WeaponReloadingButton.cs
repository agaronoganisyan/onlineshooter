using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic.WeaponReloadingButtonLogic
{
    public class WeaponReloadingButton : InputButton
    {
        [SerializeField] private Image _icon;
        
        private void Start()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _equipment.OnCurrentWeaponChanged += ChangeWeapon;
            _equipment.OnCurrentWeaponAmmoChanged += AmmoChanged;
            
            _inputHandler.OnReloadingInputStatusChanged += SetEnableStatus;
        }

        protected override void Prepare()
        {
            ChangeWeapon(_equipment.CurrentWeaponInfo);
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
            if(_equipment.IsWeaponReloadingPossible()) Enable();
            else Disable();
        }

        private void ChangeWeapon(WeaponConfig weaponInfo)
        {
            SetIconSprite(weaponInfo.BulletIconSprite);
        }

        private void SetIconSprite(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}