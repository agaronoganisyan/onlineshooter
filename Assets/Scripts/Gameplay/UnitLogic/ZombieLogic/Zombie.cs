using Gameplay.HealthLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.UnitLogic.RagdollLogic;
using Gameplay.UnitLogic.ZombieLogic.AnimationLogic;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class Zombie : Unit
    {
        private SimpleHealthSystem _healthSystem;
        private IZombieAnimator _animator;
        private IRagdollHandler _ragdollHandler;
        
        public override void Initialize()
        {
            base.Initialize();
            _healthSystem = new SimpleHealthSystem();
            _animator = GetComponentInChildren<IZombieAnimator>();
            _ragdollHandler = GetComponentInChildren<IRagdollHandler>();

            _hitBox.Initialize(_healthSystem);
            _ragdollHandler.Initialize(_hitBox);
            
            _healthSystem.OnEnded += Die;
        }
        
        public override void SetInfo(string unitName, TeamType teamType)
        {
            _info.Set(unitName,teamType,_healthSystem, _transform, _headTransform);
            AddInfoBar();
        }

        public override void AddInfoBar()
        {
            _sharedGameplayCanvas.AddUnitInfoObject(this);
        }
        
        protected override void Prepare(SpawnPointInfo spawnPointInfo)
        {
            base.Prepare(spawnPointInfo);
            _healthSystem.Prepare(1000);
            _animator.Prepare();
            _ragdollHandler.Prepare();
            AddInfoBar();
        }

        protected override void Stop()
        {
            base.Stop();
            _animator.Stop();
        }
        
        protected override void Die()
        {
            Stop();
            _ragdollHandler.Hit();
            base.Die();
        }
    }
}