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

        public Color TeammateFirstColor => _teammateFirstColor;
        [Header("Info Colors")]
        [SerializeField] private Color _teammateFirstColor;
        public Color TeammateSecondColor => _teammateSecondColor;
        [SerializeField] private Color _teammateSecondColor;
        public Color EnemyFirstColor => _enemyFirstColor;
        [SerializeField] private Color _enemyFirstColor;
        public Color EnemySecondColor => _enemySecondColor;
        [SerializeField] private Color _enemySecondColor;
        
        public SharedGameplayCanvasObject Prefab => _prefab;
        [SerializeField] private SharedGameplayCanvasObject _prefab;
    }
}