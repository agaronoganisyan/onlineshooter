using UnityEngine;
using UnityEngine.UI;

namespace InputLogic.InputCanvasLogic.WeaponSwitchingButtonLogic
{
    public class WeaponSwitchingButton : InputButton
    {
        [SerializeField] private Image _icon;
        
        private void Start()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _equipment.OnCurrentWeaponChanged += (weapon) => ChangeWeapon();
            
            _inputHandler.OnSwitchingInputStatusChanged += SetEnableStatus;
        }

        protected override void Prepare()
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