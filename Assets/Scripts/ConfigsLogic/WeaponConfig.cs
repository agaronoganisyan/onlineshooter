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
        public Bullet BulletPrefab => _bulletPrefab;
        [SerializeField] private Bullet _bulletPrefab;
    }
}