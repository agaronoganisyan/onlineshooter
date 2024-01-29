using System;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public event Action OnDied;
        
        protected UnitInfo _info;
        protected IUnitHitBox _hitBox;
        protected ISharedGameplayCanvasSystem _sharedGameplayCanvas;
        private IUnitController _controller;

        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Transform _headTransform;

        public virtual void Initialize()
        {
            _sharedGameplayCanvas = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            
            _hitBox = GetComponent<IUnitHitBox>();
            _controller = GetComponent<IUnitController>();
            
            _controller.Initialize();
        }
        
        public virtual void Prepare(SpawnPointInfo spawnPointInfo)
        {
            enabled = true;
            _controller.Prepare(spawnPointInfo.Position,spawnPointInfo.Rotation);
            _hitBox.Prepare();
            Enable();
        }
        
        protected virtual void Stop()
        {
            enabled = false;
            _controller.Stop();
        }
        
        public abstract void SetInfo(string unitName, TeamType teamType);

        public abstract void AddInfoBar();
        
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
        
        protected virtual void Die()
        {
            OnDied?.Invoke();
        }
    }
}