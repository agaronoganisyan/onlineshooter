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
        public Transform Transform { get; private set; }
        public Transform HeadTransform { get; private set; }
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
            Transform = target;
            HeadTransform = targetHead;
        }
    }
}