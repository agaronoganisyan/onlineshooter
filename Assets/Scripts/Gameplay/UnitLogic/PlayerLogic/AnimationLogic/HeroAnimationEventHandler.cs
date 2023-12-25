using System;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public class HeroAnimationEventHandler : MonoBehaviour, IHeroAnimationEventHandler
    {
        public event Action OnReloadingFinished;
        public event Action OnSwitchWeapon;
        public event Action OnSwitchingFinished;
        public event Action OnThrow;
        public event Action OnThrowingFinished;

        public void ReloadingFinished() => OnReloadingFinished?.Invoke();
        public void SwitchWeapon() => OnSwitchWeapon?.Invoke();
        public void SwitchingWeaponFinished() => OnSwitchingFinished?.Invoke();
        public void Throw() => OnThrow?.Invoke();
        public void ThrowingFinished() => OnThrowingFinished?.Invoke();
    }
}