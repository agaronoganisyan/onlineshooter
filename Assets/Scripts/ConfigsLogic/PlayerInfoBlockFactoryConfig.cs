using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "PlayerInfoBlockFactoryConfig", menuName = "Configs/New PlayerInfoBlockFactoryConfig")]
    public class PlayerInfoBlockFactoryConfig : ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        public PlayerInfoBlock Prefab => _prefab;
        [SerializeField] private PlayerInfoBlock _prefab;
    }
}