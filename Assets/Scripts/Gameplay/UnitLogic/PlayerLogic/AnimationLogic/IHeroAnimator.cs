using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public interface IHeroAnimator
    {
        HeroAnimationEventHandler AnimationEventHandler { get; }
        void PlayMovement(Vector2 movementDirection);
        void PlayIdle();
        void PlayAim();
        void PlayReload();
        void PlayDraw();
        void PlayThrow();
        void SetRuntimeAnimatorController(RuntimeAnimatorController newController);
    }
}