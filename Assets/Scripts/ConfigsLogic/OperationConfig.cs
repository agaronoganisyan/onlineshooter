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
        public AssetReference Scene => _scene;
        [SerializeField] private AssetReference _scene;
    }
}