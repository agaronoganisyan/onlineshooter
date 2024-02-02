using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "BulletFactoryConfig", menuName = "Configs/New BulletFactoryConfig")]
    public class BulletFactoryConfig : ScriptableObject, IService
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField] private int initialPoolSize;
        public Bullet Prefab => _prefab;
        [SerializeField] private Bullet _prefab;
    }
}