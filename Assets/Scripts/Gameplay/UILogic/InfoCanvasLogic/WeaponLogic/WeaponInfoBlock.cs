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
        private WeaponConfig _weaponInfo;
        
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
            _equipment.OnCurrentWeaponAmmoChanged += UpdateAmmoInfo;
            
            _blockColorAnimationDuration = _config.BlockColorAnimationDuration;
            
            SetIconFill(1);
        }

        private void EquipmentChanged()
        {
            _weaponInfo = _equipment.GetWeaponInfoByType(_weaponType);

            _imageFillingDuration = _weaponInfo.Frequency;

            SetIconSprite(_weaponInfo.WeaponIconSprite);
            SetBlockColor(_equipment.CurrentWeaponInfo.WeaponType);
        }
        
        private void UpdateBlockInfo(WeaponConfig weaponInfo)
        {
            SetBlockColor(weaponInfo.WeaponType);
        }
        
        private void UpdateAmmoInfo(int currentCount, int maxCount)
        {
            if (_equipment.CurrentWeaponInfo.WeaponType != _weaponType) return;

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