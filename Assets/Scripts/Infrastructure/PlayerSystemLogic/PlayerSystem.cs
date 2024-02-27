using System;
using Gameplay.UnitLogic.PlayerLogic;

namespace Infrastructure.PlayerSystemLogic
{
    public class PlayerSystem : IPlayerSystem
    {
        public event Action<Player> OnSpawned;
        public event Action OnDespawned;
        public event Action OnDied;
        public Player Player => _player;
        private Player _player;
        
        public void Initialize()
        {
            
        }
        
        public void SetPlayer(Player player)
        {
            if (_player != null) UnsubscribeFromThisPlayer(_player);

            _player = player;
            
            SubscribeToThisPlayer(_player);
            
            OnSpawned?.Invoke(_player);
        }
        
        private void SubscribeToThisPlayer(Player player)
        {
            player.OnDied += Died;
        }
        
        private void UnsubscribeFromThisPlayer(Player player)
        {
            player.OnDied -= Died;
        }

        private void Died() => OnDied?.Invoke();
    }
}