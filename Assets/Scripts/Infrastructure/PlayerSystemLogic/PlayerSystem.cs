using System;
using Gameplay.UnitLogic.PlayerLogic;

namespace Infrastructure.PlayerSystemLogic
{
    public class PlayerSystem : IPlayerSystem
    {
        public event Action<Player> OnSpawned;
        public event Action OnMoved;
        public event Action OnDespawned;
        public event Action OnDied;
        public event Action<float, float> OnHealthChanged;
        public event Action OnHealthBelowCriticalThreshold;
        public event Action OnHealthAboveCriticalThreshold;
        public event Action OnHealthEnded;
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
            player.OnMoved += Moved;
            player.HealthSystem.OnChanged += HealthChanged;
            player.HealthSystem.OnBelowCriticalThreshold += HealthBelowCriticalThreshold;
            player.HealthSystem.OnAboveCriticalThreshold += HealthAboveCriticalThreshold;
            player.HealthSystem.OnEnded += HealthEnded;
        }
        
        private void UnsubscribeFromThisPlayer(Player player)
        {
            player.OnDied -= Died;
            player.OnMoved -= Moved;
            player.HealthSystem.OnChanged -= HealthChanged;
            player.HealthSystem.OnBelowCriticalThreshold -= HealthBelowCriticalThreshold;
            player.HealthSystem.OnAboveCriticalThreshold -= HealthAboveCriticalThreshold;
            player.HealthSystem.OnEnded -= HealthEnded;
        }

        private void Died() => OnDied?.Invoke();
        private void Moved() => OnMoved?.Invoke();

        private void HealthChanged(float currentAmount, float maxAmount) => OnHealthChanged?.Invoke(currentAmount, maxAmount);

        private void HealthBelowCriticalThreshold() => OnHealthBelowCriticalThreshold?.Invoke();

        private void HealthAboveCriticalThreshold() => OnHealthAboveCriticalThreshold?.Invoke();

        private void HealthEnded() => OnHealthEnded?.Invoke();
    }
}