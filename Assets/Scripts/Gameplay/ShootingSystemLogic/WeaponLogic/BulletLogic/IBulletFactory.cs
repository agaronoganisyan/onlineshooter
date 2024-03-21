using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public interface IBulletFactory : IService
    {
        void Initialize();
        Bullet Get();
        Bullet Get(HitInfo hitInfo, Vector3 startPosition, Vector3 direction, float speed, float lifeTime);
        void ReturnAllObjectToPool();
    }
}