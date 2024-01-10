using Gameplay.HealthLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public class UnitHitBox : MonoBehaviour, IUnitHitBox
    {
        private HealthSystem _healthSystem;
        
        public void Initialize(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
        }
        
        public void TakeDamage(RifleBullet bullet)
        {
            _healthSystem.Decrease(bullet.Damage);
        }

        public void TakeDamage(Classic grenade)
        {
        }
    }
}