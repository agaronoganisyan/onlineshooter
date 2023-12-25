using System;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public interface IHeroAnimationEventHandler
    {
        event Action OnReloadingFinished;
        event Action OnSwitchWeapon;
        event Action OnSwitchingFinished;
        event Action OnThrow;
        event Action OnThrowingFinished;
    }
}