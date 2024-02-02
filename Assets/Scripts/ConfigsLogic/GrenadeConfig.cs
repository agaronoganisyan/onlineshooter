using Gameplay.ShootingSystemLogic.GrenadeLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "GrenadeConfig", menuName = "Configs/New GrenadeConfig")]
    public class GrenadeConfig : ScriptableObject
    {
        public GrenadeType GrenadeType => _grenadeType;
        [SerializeField] private GrenadeType _grenadeType;
        public float Damage => _damage;
        [SerializeField] private float _damage;
        public float ImpactRadius => _impactRadius;
        [SerializeField] private float _impactRadius;
        public float ReloadingDuration => _reloadingDuration;
        [SerializeField] private float _reloadingDuration;
    }
}