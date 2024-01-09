using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "HealthSystemConfig", menuName = "Configs/New HealthSystemConfig")]
    public class HealthSystemConfig: ScriptableObject, IService
    {
        public float CriticalHealthThreshold => _criticalHealthThreshold;
        [SerializeField] private float _criticalHealthThreshold;
        public float DelayBeforeRegeneration => _delayBeforeRegeneration;
        [SerializeField] private float _delayBeforeRegeneration;
        public float RegenerationDegree => _regenerationDegree;
        [SerializeField] private float _regenerationDegree;
        public float RegenerationFrequency => _regenerationFrequency;
        [SerializeField] private float _regenerationFrequency;
    }
}