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
        Player Player { get; }
        void Initialize();
        void SetPlayer(Player player);
    }
}