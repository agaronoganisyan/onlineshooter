using Fusion;
using Gameplay.UnitLogic.DamageLogic;
using UnityEngine;

namespace NetworkLogic.HitLogic
{
    public struct HitUtility
    {
        public static IDamageable GetHitTarget(Hitbox hitbox, Collider collider)
        {
            if (hitbox != null)
                return hitbox.Root.GetComponent<IDamageable>();

            if (collider == null)
                return null;

            return collider.GetComponentInParent<IDamageable>();
        }
    }
}