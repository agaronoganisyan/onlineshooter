using System;
using Fusion;
using Gameplay.HealthLogic;
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
        
        [Networked] public ref UnitInfo Info => ref MakeRef<UnitInfo>();
        
        protected IUnitHitBox _hitBox;
        protected ISharedGameplayCanvasSystem _sharedGameplayCanvas;
        private IMatchSystem _matchSystem;
        private IUnitController _controller;
        
        public Transform Transform => _transform;
        [SerializeField] protected Transform _transform;
        public Transform HeadTransform => _headTransform;
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
            _controller.Initialize();
            
            if (!HasStateAuthority) return;

            _matchSystem.OnFinished += Stop;
        }

        // private void Start()
        // {
        //     Disable();
        // }

        public abstract void SetInfo(TeamType teamType, string unitName);

        public abstract HealthSystem GetHealthSystem();
        
        protected abstract void AddInfoBar();

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
            _controller.Prepare(spawnPointInfo.Position, spawnPointInfo.Rotation);
            _hitBox.Prepare();
            Enable();
        }

        protected virtual void Stop()
        {
            enabled = false;
            
            //if (!HasStateAuthority) return;
            
            _controller.Stop();
        }

        protected virtual void Update()
        {
            //_controller.Tick();
        }

        public override void Render()
        {
            base.Render();
        }

        protected virtual void Die()
        {
            OnDied?.Invoke();
        }
    }
}