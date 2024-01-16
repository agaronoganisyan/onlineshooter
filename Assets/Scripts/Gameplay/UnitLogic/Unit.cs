using UnityEngine;

namespace Gameplay.UnitLogic
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        protected IUnitHitBox _hitBox;

        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Transform _headTransform;

        public virtual void Initialize()
        {
            _hitBox = GetComponent<IUnitHitBox>();
        }
        
        public virtual void Prepare()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void Update()
        {
            
        }
    }
}