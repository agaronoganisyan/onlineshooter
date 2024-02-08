using Gameplay.EffectsLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "GrenadeEffectFactoryConfig", menuName = "Configs/New GrenadeEffectFactoryConfig")]
    public class GrenadeEffectFactoryConfig : ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        public Effect Prefab => _prefab;
        [SerializeField] private Effect _prefab;
    }
}