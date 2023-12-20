using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "ShootingSystemConfig", menuName = "Configs/New ShootingSystemConfig")]
    public class ShootingSystemConfig : ScriptableObject
    {
        public float DetectionFrequency => _detectionFrequency;
        [SerializeField, Tooltip("How many seconds should pass for the next detection")] private float _detectionFrequency;
        public Vector3 OffsetForTargetShooting => _offsetForTargetShooting;
        [SerializeField] private Vector3 _offsetForTargetShooting;
        public int MaxDetectingCollidersAmount => _maxDetectingCollidersAmount;
        [SerializeField] private int _maxDetectingCollidersAmount;
        public int MinAngleToStartingShooting => minAngleToStartingShooting;
        [SerializeField] private int minAngleToStartingShooting;
        public LayerMask TargetHitLayer => _targetHitLayer;
        [SerializeField] private LayerMask _targetHitLayer;
        public LayerMask ObstacleLayer => _obstacleLayer;
        [SerializeField] private LayerMask _obstacleLayer;
    }
}