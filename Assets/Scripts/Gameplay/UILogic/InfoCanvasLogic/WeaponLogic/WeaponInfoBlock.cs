using System;
using ConfigsLogic;
using DG.Tweening;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic.InfoCanvasLogic.WeaponLogic
{
    public class WeaponInfoBlock : FillableImage, IWeaponInfoBlock
    {
        [SerializeField] private WeaponInfoBlockConfig _config;
        
        [SerializeField] private WeaponType _weaponType;
        private IEquipment _equipment;
        private Weapon _weapon;
        
        [SerializeField] private Image _iconBackground;
        [SerializeField] private Image _selectionIndicator;

        private float _blockColorAnimationDuration;
        
        private void Awake()
        {
            Initialize();
        }

        private  void Initialize()
        {
            _equipment = ServiceLocator.Get<IEquipment>();
            
            _equipment.OnEquipmentChanged += EquipmentChanged;
            _equipment.OnCurrentWeaponChanged += UpdateBlockInfo;

            _blockColorAnimationDuration = _config.BlockColorAnimationDuration;
            
            SetIconFill(1);
        }

        private void EquipmentChanged()
        {
            if (_weapon != null) _weapon.OnAmmoChanged -= UpdateAmmoInfo;
            
            _weapon = _equipment.GetWeaponByType(_weaponType);
            _weapon.OnAmmoChanged += UpdateAmmoInfo;

            _imageFillingDuration = _weapon.WeaponConfig.Frequency;

            SetIconSprite(_weapon.WeaponConfig.WeaponIconSprite);
            SetBlockColor(_equipment.CurrentWeapon.WeaponConfig.WeaponType);
        }
        
        private void UpdateBlockInfo(Weapon weapon)
        {
            SetBlockColor(weapon.WeaponConfig.WeaponType);
        }
        
        private void UpdateAmmoInfo(int currentCount, int maxCount)
        {
            SetIconFill((float)currentCount / maxCount,true);
        }

        private void SetIconSprite(Sprite icon)
        {
            _fillableImage.sprite = icon;
            _iconBackground.sprite = icon;

            _fillableImage.SetNativeSize();
            _iconBackground.SetNativeSize();
        }

        private void SetBlockColor(WeaponType type)
        {
            _selectionIndicator.DOComplete();
            _selectionIndicator.DOFade(GetSelectionIndicatorFadeValue(type), _blockColorAnimationDuration);
        }

        int GetSelectionIndicatorFadeValue(WeaponType currentWeaponType)
        {
            return currentWeaponType == _weaponType ? 1 : 0;
        }
    }
}