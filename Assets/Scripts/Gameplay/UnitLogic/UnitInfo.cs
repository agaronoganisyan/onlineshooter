using Gameplay.HealthLogic;
using Gameplay.MatchLogic.TeamsLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public struct UnitInfo
    {
        public string Name { get; private set; }
        public TeamType TeamType{ get; private set; }
        public HealthSystem HealthSystem { get; private set; }
        public Transform Target { get; private set; }
        public Transform TargetHead { get; private set; }
        public void Set(
            string unitName,
            TeamType teamType,
            HealthSystem healthSystem,
            Transform target,
            Transform targetHead)
        {
            Name = unitName;
            TeamType = teamType;
            HealthSystem = healthSystem;
            Target = target;
            TargetHead = targetHead;
        }
    }
}