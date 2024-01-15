using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "PlayerInfoBlockConfig", menuName = "Configs/New PlayerInfoBlockConfig")]
    public class PlayerInfoBlockConfig : ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        
        public Vector3 OffsetToTarget => _offsetToTarget;
        [SerializeField] private Vector3 _offsetToTarget;

        public SharedGameplayCanvasObject Prefab => _prefab;
        [SerializeField] private SharedGameplayCanvasObject _prefab;
    }
}