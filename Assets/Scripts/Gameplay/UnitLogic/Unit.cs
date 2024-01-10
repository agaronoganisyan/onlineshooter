using UnityEngine;

namespace Gameplay.UnitLogic
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] protected UnitHitBox _hitBox;

        [SerializeField] protected Transform _transform;
        
        protected virtual void Awake()
        {
        }
    }
}