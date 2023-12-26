using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public class UnitHitBox : MonoBehaviour, IUnitHitBox
    {
        public void TakeDamage(RifleBullet bullet)
        {
        }

        public void TakeDamage(Classic grenade)
        {
        }
    }
}