using ConfigsLogic;
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
        private IShootingSystem _shootingSystem;
        private ISpawnSystem _spawnSystem;
        private PlayerHealthSystem _healthSystem;
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
            if (!HasStateAuthority) return;
            
            Debug.LogError("PLAYER_SPAWNED_METHOD");
            
            base.Spawned();
            
            _hitBox.Initialize(_healthSystem);
            _healthSystem.Initialize();
            _shootingSystem.Initialize();
            _ragdollHandler.Initialize(_hitBox);
                
            _spawnSystem.OnSpawned += Prepare;
            _healthSystem.OnEnded += Die;
        }
        
        public override void Update()
        {
            if (!HasStateAuthority) return;
            
            base.Update();
            _shootingSystem.Tick();
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                Die();
            }
        }
        
        public override void SetInfo(string unitName, TeamType teamType)
        {
            _info.Set(unitName,teamType,_healthSystem, _transform, _headTransform);
        }

        public override void AddInfoBar()
        {
            //_sharedGameplayCanvas.AddObjectAddObject(_info);
        }
        
        protected override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            base.Prepare(spawnPointInfo);
            _healthSystem.Prepare(1000);
            _shootingSystem.Prepare();
            _playerAnimator.Prepare();
            _ragdollHandler.Prepare();
            AddInfoBar();
        }
        
        protected override void Stop()
        {
            base.Stop();
            _shootingSystem.Stop();
            _playerAnimator.Stop();
        }
        
        protected override void Die()
        {
            Stop();
            _ragdollHandler.Hit();
            base.Die();
        }
    }
}