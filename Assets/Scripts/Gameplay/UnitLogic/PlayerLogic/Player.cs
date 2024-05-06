using System;
using System.Drawing;
using Fusion;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.ShootingSystemLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Gameplay.UnitLogic.RagdollLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class Player : Unit
    {
        public Action OnMoved;
        
        public PlayerHealthSystem HealthSystem => _healthSystem;
        private PlayerHealthSystem _healthSystem;
        
        private IShootingSystem _shootingSystem;
        private ISpawnSystem _spawnSystem;
        private IPlayerAnimator _playerAnimator;
        private IRagdollHandler _ragdollHandler;

        public override void Awake()
        {
            base.Awake();
            
            _shootingSystem = GetComponent<IShootingSystem>();
            _playerAnimator = GetComponentInChildren<IPlayerAnimator>();
            _ragdollHandler = GetComponentInChildren<IRagdollHandler>();
            _healthSystem = GetComponent<PlayerHealthSystem>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
        }

        public override void Spawned()
        {
            base.Spawned();
            _hitBox.Initialize(this);
            _healthSystem.Initialize();
            _ragdollHandler.Initialize(_hitBox);

            _healthSystem.OnEnded += Die;
            
            if (!HasStateAuthority) return;

            _shootingSystem.Initialize();

            _spawnSystem.OnSpawned += Prepare; 
        }

        protected override void Update()
        {
            if (!HasStateAuthority) return;
            base.Update();
            //_shootingSystem.FixedTick();
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                _healthSystem.Decrease(200);
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority) return;
            
            OnMoved?.Invoke();
            base.FixedUpdateNetwork();
            _shootingSystem.FixedTick();
        }

        // public override void Render()
        // {
        //     base.Render();
        //     if (!HasStateAuthority) return;
        //     
        //     base.Update();
        //     _shootingSystem.Tick();
        //     
        //     if (Input.GetKeyDown(KeyCode.I))
        //     {
        //         _healthSystem.Decrease(200);
        //     }
        // }
        
        public override void SetInfo(TeamType teamType, string unitName)
        {
            //if (!HasStateAuthority) return;

            RPC_SetInfo(teamType, unitName);
        }
        
        public override HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }

        protected override void AddInfoBar()
        {
            if (HasStateAuthority) return;
            
            _sharedGameplayCanvas.AddUnitInfoObject(this);
        }

        protected override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            if (!HasStateAuthority) return;
            
            base.Prepare(spawnPointInfo);
            _healthSystem.Prepare(1500);
            _shootingSystem.Prepare();
            
            RPC_Prepare();
        }

        protected override void Stop()
        {
            base.Stop();
            _playerAnimator.Stop(); 
            
            if (!HasStateAuthority) return;
            
            _shootingSystem.Stop();
        }
        
        protected override void Die()
        {
            Stop();
            _ragdollHandler.Hit();
            base.Die();
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_SetInfo(TeamType teamType, string unitName)
        {
            Info.Set(unitName, teamType);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Prepare()
        {
            _playerAnimator.Prepare();
            _ragdollHandler.Prepare();
            AddInfoBar();
        }
    }
}