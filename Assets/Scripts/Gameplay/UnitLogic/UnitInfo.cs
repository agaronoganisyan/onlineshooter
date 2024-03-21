using Fusion;
using Gameplay.HealthLogic;
using Gameplay.MatchLogic.TeamsLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public struct UnitInfo : INetworkStruct
    {
        [Networked, Capacity(24)] public string Name { get => default; set {} }
        public TeamType TeamType{ get; private set; }

        public void Set(string unitName, TeamType teamType)
        {
            Name = unitName;
            TeamType = teamType;
        }
    }
}