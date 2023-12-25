using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using UnityEngine;

namespace ConfigsLogic
{
   [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/New WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        public WeaponType WeaponType => _weaponType;
        [SerializeField] private WeaponType _weaponType;
        public WeaponModelType ModelType => _modelType;
        [SerializeField] private WeaponModelType _modelType;
        public float DetectionZoneAngle => _detectionZoneAngle;
        [SerializeField] private float _detectionZoneAngle;
        public float DetectionZoneRadius => _detectionZoneRadius;
        [SerializeField] private float _detectionZoneRadius;
        public float Damage => _damage;
        [SerializeField] private float _damage;
        public float BulletSpeed => _bulletSpeed;
        [SerializeField] private float _bulletSpeed;
        public int MaxAmmoCount => _maxAmmoCount;
        [SerializeField] private int _maxAmmoCount;
        public float Frequency => _frequency;
        [SerializeField, Tooltip("How many seconds should pass for the next shot")] private float _frequency;
        public float ReloadingDuration => _reloadingDuration;
        [SerializeField] private float _reloadingDuration;
        public Vector3 PositionInHand => _positionInHand;
        [Header("Hand Block")]
        [SerializeField] private Vector3 _positionInHand;
        public Vector3 RotationInHand => _rotationInHand;
        [SerializeField] private Vector3 _rotationInHand;
        public Vector3 PositionInContainer => _positionInContainer;
        [Header("Container Block")]
        [SerializeField] private Vector3 _positionInContainer;
        public Vector3 RotationInContainer => _rotationInContainer;
        [SerializeField] private Vector3 _rotationInContainer;
        public Bullet BulletPrefab => _bulletPrefab;
        [Space(25)]
        [SerializeField] private Bullet _bulletPrefab;
        public AnimatorOverrideController AnimatorOverride => _animatorOverride;
        [SerializeField] private AnimatorOverrideController _animatorOverride;
    }
}