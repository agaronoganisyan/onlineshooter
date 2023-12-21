using Gameplay.UnitLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class RifleBullet : Bullet
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(this);
                ReturnToPool();
            }
        }
    }
}