using System;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;

namespace Infrastructure.PlayerSystemLogic
{
    public interface IPlayerSystem : IService
    {
        event Action<Player> OnSpawned;
        event Action OnDespawned;
        event Action OnDied;
        event Action<float, float> OnHealthChanged;
        event Action OnHealthBelowCriticalThreshold;
        event Action OnHealthAboveCriticalThreshold;
        event Action OnHealthEnded;
        Player Player { get; }
        void Initialize();
        void SetPlayer(Player player);
    }
}