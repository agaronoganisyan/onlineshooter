using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "SpawnPointSystemConfig", menuName = "Configs/New SpawnPointSystemConfig")]
    public class SpawnPointSystemConfig : ScriptableObject
    {
        public int MaxDetectingCollidersAmount => _maxDetectingCollidersAmount;
        [SerializeField] private int _maxDetectingCollidersAmount;
        public float DetectionZoneRadius => _detectionZoneRadius;
        [SerializeField] private float _detectionZoneRadius;
        public LayerMask TargetHitLayer => _targetHitLayer;
        [SerializeField] private LayerMask _targetHitLayer;
    }
}