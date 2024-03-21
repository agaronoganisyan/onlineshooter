using Fusion;
using Gameplay.MatchLogic.TeamsLogic;

namespace Gameplay.UnitLogic.DamageLogic
{
    public struct HitInfo : INetworkStruct
    {
        public TeamType TeamType { get; private set; }
        public float Damage { get; private set; }

        public HitInfo(TeamType teamType, float damage)
        {
            TeamType = teamType;
            Damage = damage;
        }
    }
}