using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        protected IUnitHitBox _hitBox;
        private IUnitController _controller;

        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Transform _headTransform;

        public virtual void Initialize()
        {
            _hitBox = GetComponent<IUnitHitBox>();
            _controller = GetComponent<IUnitController>();
            
            _controller.Initialize();
        }
        
        public virtual void Prepare(SpawnPointInfo spawnPointInfo)
        {
            _controller.Prepare(spawnPointInfo.Position,spawnPointInfo.Rotation);
            Enable();
        }
        
        protected virtual void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void Update()
        {
            _controller.Tick();
        }
    }
}