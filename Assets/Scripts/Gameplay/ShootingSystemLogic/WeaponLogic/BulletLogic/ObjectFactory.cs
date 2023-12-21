using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public abstract class ObjectFactory<T> : IFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly IPool<T> _pool;

        protected ObjectFactory(T prefab, int initialSize)
        {
            _pool = new ObjectPool<T>(prefab,initialSize);
        }

        public virtual T Get()
        {
            return _pool.Pull();
        }
    }
}