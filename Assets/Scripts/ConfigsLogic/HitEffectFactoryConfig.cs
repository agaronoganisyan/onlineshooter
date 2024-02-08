using Gameplay.EffectsLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "HitEffectFactoryConfig", menuName = "Configs/New HitEffectFactoryConfig")]
    public class HitEffectFactoryConfig : ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        public Effect Prefab => _prefab;
        [SerializeField] private Effect _prefab;
    }
}