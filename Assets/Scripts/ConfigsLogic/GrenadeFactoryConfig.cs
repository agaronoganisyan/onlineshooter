using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "GrenadeFactoryConfig", menuName = "Configs/New GrenadeFactoryConfig")]
    public class GrenadeFactoryConfig: ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        public Grenade Prefab => _prefab;
        [SerializeField] private Grenade _prefab;
    }
}