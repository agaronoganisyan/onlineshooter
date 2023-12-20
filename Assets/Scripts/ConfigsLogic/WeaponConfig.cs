using UnityEngine;

namespace ConfigsLogic
{
   [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/New WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        public float DetectionZoneAngle => _detectionZoneAngle;
        [SerializeField] private float _detectionZoneAngle;
        public float DetectionZoneRadius => _detectionZoneRadius;
        [SerializeField] private float _detectionZoneRadius;
    }
}