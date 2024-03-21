using Gameplay.HealthLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.ShootingSystemLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Gameplay.UnitLogic.RagdollLogic;
using Gameplay.UnitLogic.ZombieLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class Zombie : Unit
    {
        private IShootingSystem _shootingSystem;
        private SimpleHealthSystem _healthSystem;
        private IPlayerAnimator _animator;
        private IRagdollHandler _ragdollHandler;

        public override void Awake()
        {
            base.Awake();
            
            _healthSystem = GetComponent<SimpleHealthSystem>();
            _shootingSystem = GetComponent<IShootingSystem>();
            _animator = GetComponent<IPlayerAnimator>();
            _ragdollHandler = GetComponentInChildren<IRagdollHandler>();

            _hitBox.Initialize(this);
            _shootingSystem.Initialize();
            _ragdollHandler.Initialize(_hitBox);
            
            _healthSystem.OnEnded += Die;
        }
        
        public override void SetInfo(TeamType teamType, string unitName)
        {
            Info.Set(unitName,teamType);
        }

        public override HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }

        protected override void AddInfoBar()
        {
            _sharedGameplayCanvas.AddUnitInfoObject(this);
        }
        
        protected override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            base.Prepare(spawnPointInfo);
            _healthSystem.Prepare(2000);
            _shootingSystem.Prepare();
            _animator.Prepare();
            _ragdollHandler.Prepare();
            AddInfoBar();
        }

        protected override void Stop()
        {
            base.Stop();
            _shootingSystem.Stop();
            _animator.Stop();
        }
        
        protected override void Die()
        {
            Stop();
            _ragdollHandler.Hit(); // Hit();
            base.Die();
        }

        protected override void Update()
        {
            base.Update();
            _shootingSystem.FixedTick();

            if (Input.GetKeyDown(KeyCode.L)) _healthSystem.Decrease(100);
        }
    }
}