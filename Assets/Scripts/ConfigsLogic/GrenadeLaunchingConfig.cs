using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "GrenadeLaunchingConfig", menuName = "Configs/New GrenadeLaunchingConfig")]
    public class GrenadeLaunchingConfig : ScriptableObject
    {
        public float LaunchingAngle => _launchingAngle;
        [SerializeField] private float _launchingAngle;
        public int MaxDetectingCollidersAmount => _maxDetectingCollidersAmount;
        [SerializeField] private int _maxDetectingCollidersAmount;
        public float CameraDetectionRadius => _cameraDetectionRadius;
        [SerializeField] private float _cameraDetectionRadius;
        public LayerMask TargetHitLayer => _targetHitLayer;
        [SerializeField] private LayerMask _targetHitLayer;
        public LayerMask ObstacleLayer => _obstacleLayer;
        [SerializeField] private LayerMask _obstacleLayer;
        public LayerMask CameraLayer => _cameraLayer;
        [SerializeField] private LayerMask _cameraLayer;
    }
}