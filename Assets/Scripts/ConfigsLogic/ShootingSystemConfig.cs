using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "ShootingSystemConfig", menuName = "Configs/New ShootingSystemConfig")]
    public class ShootingSystemConfig : ScriptableObject, IService
    {
        public float DetectionFrequency => _detectionFrequency;
        [SerializeField, Tooltip("How many seconds should pass for the next detection")] private float _detectionFrequency;
        public Vector3 OffsetForTargetShooting => _offsetForTargetShooting;
        [SerializeField] private Vector3 _offsetForTargetShooting;
        public float AimingSpeed => _aimingSpeed;
        [SerializeField] private float _aimingSpeed;
        public int MinAngleToStartingShooting => minAngleToStartingShooting;
        [SerializeField] private int minAngleToStartingShooting;
        public float GrenadeLaunchingAngle => _grenadeLaunchingAngle;
        [SerializeField] private float _grenadeLaunchingAngle;
        public float CameraDetectionRadius => _cameraDetectionRadius;
        [SerializeField] private float _cameraDetectionRadius;
        public int MaxDetectingCollidersAmount => _maxDetectingCollidersAmount;
        [SerializeField] private int _maxDetectingCollidersAmount;
        public LayerMask TargetHitLayer => _targetHitLayer;
        [SerializeField] private LayerMask _targetHitLayer;
        public LayerMask ObstacleLayer => _obstacleLayer;
        [SerializeField] private LayerMask _obstacleLayer;
        public LayerMask CameraLayer => _cameraLayer;
        [SerializeField] private LayerMask _cameraLayer;
    }
}