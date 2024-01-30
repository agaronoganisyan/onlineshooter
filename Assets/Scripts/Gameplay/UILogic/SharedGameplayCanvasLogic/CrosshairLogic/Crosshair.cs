using DG.Tweening;
using Gameplay.CameraLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.CrosshairLogic
{
    public class Crosshair : MonoBehaviour
    {
        private IAimSystem _aimSystem;
        private IEquipment _equipment;
        private Weapon _weapon;

        private Camera _gameplayCamera;
        
        [SerializeField] private CrosshairReloader _reloader;
        [SerializeField] private Image[] _crosshairSides;

        private RectTransform _transform;
        
        private float _crosshairSidesAnimationDuration;

        private int _crosshairSidesCount;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _gameplayCamera = ServiceLocator.Get<IGameplayCamera>().Camera;
            _aimSystem = ServiceLocator.Get<IAimSystem>();
            _equipment = ServiceLocator.Get<IEquipment>();

            _transform = GetComponent<RectTransform>();
            
            _aimSystem.OnAimPositionChanged += AimPositionChanged;
            
            _equipment.OnEquipmentChanged += EquipmentChanged;
            _equipment.OnCurrentWeaponChanged += WeaponChanged;
            _equipment.OnCurrentWeaponFired += WasFire;
            
            _equipment.OnCurrentWeaponReloadingStarted += StartRelaoder;
            _equipment.OnCurrentWeaponReloadingFinished += StopRelaoder;

            _crosshairSidesCount = _crosshairSides.Length;

            _reloader.Initialize();
        }

        private void AimPositionChanged(Vector3 position)
        {
            _transform.position = DetectionOnScreenFunctions.GetScreenPosition(_gameplayCamera, position);;
        }

        private void EquipmentChanged()
        {
            _weapon = _equipment.CurrentWeapon;

            WeaponChanged(_weapon);
        }

        private void WasFire()
        {
            for (int i = 0; i < _crosshairSidesCount; i++)
            {
                _crosshairSides[i].fillAmount = 0;
                _crosshairSides[i].DOFillAmount(1, _crosshairSidesAnimationDuration);
            }
        }
        
        private void WeaponChanged(Weapon weapon)
        {
            _weapon = weapon;
            _crosshairSidesAnimationDuration = _weapon.WeaponConfig.Frequency;

            StopRelaoder();
        }

        private void StartRelaoder()
        {
            _reloader.Play();
        }
        
        private void StopRelaoder()
        {
            _reloader.Stop();

        }
    }
}