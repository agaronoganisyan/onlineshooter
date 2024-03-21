using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public interface IGrenadeFactory : IService
    {
        void Initialize();
        Grenade Get();
        Grenade Get(HitInfo hitInfo, Vector3 startPosition, Vector3 targetPosition, float impactRadius);
        void ReturnAllObjectToPool();
    }
}