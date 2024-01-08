using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "WeaponInfoBlockConfig", menuName = "Configs/New WeaponInfoBlockConfig")]
    public class WeaponInfoBlockConfig : ScriptableObject
    {
        public float BlockColorAnimationDuration => _blockColorAnimationDuration;
        [SerializeField] private float _blockColorAnimationDuration;
    }
}