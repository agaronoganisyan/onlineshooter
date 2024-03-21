using System;
using Fusion;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public class HeroAnimationEventHandler : NetworkBehaviour, IHeroAnimationEventHandler
    {
        public event Action OnReloadingFinished;
        public event Action OnSwitchWeapon;
        public event Action OnSwitchingFinished;
        public event Action OnThrow;
        public event Action OnThrowingFinished;
        
        public void ReloadingFinished()
        {
            if (!HasStateAuthority) return;
            
            OnReloadingFinished?.Invoke();
        }
        public void SwitchWeapon()
        {
            if (!HasStateAuthority) return;

            OnSwitchWeapon?.Invoke();
        }

        public void SwitchingWeaponFinished()
        {
            if (!HasStateAuthority) return;

            OnSwitchingFinished?.Invoke();
        }

        public void Throw()
        {
            if (!HasStateAuthority) return;

            OnThrow?.Invoke();   
        }

        public void ThrowingFinished()
        {
            if (!HasStateAuthority) return;

            OnThrowingFinished?.Invoke();
        }
    }
}