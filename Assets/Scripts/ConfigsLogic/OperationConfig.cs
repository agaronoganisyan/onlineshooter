using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ConfigsLogic
{
    public enum OperationType
    {
        None,
        Deathmatch
    }
    [CreateAssetMenu(fileName = "OperationConfig", menuName = "Configs/New OperationConfig")]
    public class OperationConfig : ScriptableObject
    {
        public OperationType Type => _type;
        [SerializeField] private OperationType _type;
        public string Task => _task;
        [SerializeField] private string _task;
        public float Duration => _duration;
        [SerializeField, Tooltip("Duration in seconds")] private float _duration;
        public AssetReference Scene => _scene;
        [SerializeField] private AssetReference _scene;
    }
}