using Gameplay.HealthLogic;
using Gameplay.MatchLogic.TeamsLogic;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class Zombie : Unit
    {
        protected SimpleHealthSystem _healthSystem;

        public override void Initialize()
        {
            base.Initialize();
            _healthSystem = new SimpleHealthSystem();
            _hitBox.Initialize(_healthSystem);
            _healthSystem.Setup(1000);
        }
        
        public override void SetInfo(string unitName, TeamType teamType)
        {
            _info.Set(unitName,teamType,_healthSystem, _transform, _headTransform);
        }

        public override void AddInfoBar()
        {
            _sharedGameplayCanvas.AddObjectAddObject(_info);
        }
    }
}