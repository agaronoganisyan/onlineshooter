using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public abstract class ObjectFactory<T> : IFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        protected IPool<T> _pool;

        public abstract void Initialize();

        public T Get()
        {
            return _pool.Pull();
        }

        public void ReturnAllObjectToPool()
        {
            _pool.PushBackAll();
        }
    }
}