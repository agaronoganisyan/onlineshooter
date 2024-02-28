using System;
using Fusion;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public abstract class Unit : NetworkBehaviour, IUnit
    {
        public event Action OnDied;
        
        public UnitInfo Info => _info;
        protected UnitInfo _info;
        protected IUnitHitBox _hitBox;
        protected ISharedGameplayCanvasSystem _sharedGameplayCanvas;
        private IMatchSystem _matchSystem;
        private IUnitController _controller;

        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Transform _headTransform;
        
        public virtual void Awake()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _sharedGameplayCanvas = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            
            _hitBox = GetComponent<IUnitHitBox>();
            _controller = GetComponent<IUnitController>();
        }

        public override void Spawned()
        {
            if (!HasStateAuthority) return;

            _controller.Initialize();

            _matchSystem.OnFinished += Stop;
        }
        
        public abstract void SetInfo(string unitName, TeamType teamType);

        public abstract void AddInfoBar();

        private void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Prepare(SpawnPointInfo spawnPointInfo)
        {
            enabled = true;
            _controller.Prepare(spawnPointInfo.Position,spawnPointInfo.Rotation);
            _hitBox.Prepare();
            Enable();
        }

        protected virtual void Stop()
        {
            enabled = false;
            
            if (!HasStateAuthority) return;
            
            _controller.Stop();
        }

        public virtual void Update()
        {
            //_controller.Tick();
        }
        
        protected virtual void Die()
        {
            OnDied?.Invoke();
        }
    }
}