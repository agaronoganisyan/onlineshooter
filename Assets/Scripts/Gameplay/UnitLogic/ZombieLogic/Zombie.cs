using Gameplay.HealthLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.ShootingSystemLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Gameplay.UnitLogic.RagdollLogic;
using Gameplay.UnitLogic.ZombieLogic.AnimationLogic;

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
            _healthSystem = new SimpleHealthSystem();
            _shootingSystem = GetComponent<IShootingSystem>();
            _animator = GetComponentInChildren<IPlayerAnimator>();
            _ragdollHandler = GetComponentInChildren<IRagdollHandler>();

            _hitBox.Initialize(_healthSystem);
            _shootingSystem.Initialize();
            _ragdollHandler.Initialize(_hitBox);
            
            _healthSystem.OnEnded += Die;
        }
        
        public override void SetInfo(string unitName, TeamType teamType)
        {
            _info.Set(unitName,teamType,_healthSystem, _transform, _headTransform);
        }

        public override void AddInfoBar()
        {
            _sharedGameplayCanvas.AddUnitInfoObject(this);
        }
        
        protected override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            base.Prepare(spawnPointInfo);
            _healthSystem.Prepare(1000);
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
            _ragdollHandler.Hit();
            base.Die();
        }
        
        public override void Update()
        {
            base.Update();
            _shootingSystem.Tick();
        }
    }
}