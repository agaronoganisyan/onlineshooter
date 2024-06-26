using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public interface IPlayerAnimator
    {
        HeroAnimationEventHandler AnimationEventHandler { get; }
        void Prepare();
        void Stop();
        void PlayMovement(Vector2 movementDirection);
        void PlayIdle();
        void PlayAim();
        void PlayReload();
        void PlayDrawFirstPart();
        void PlayDrawSecondPart();
        void PlayThrow();
        void SetRuntimeAnimatorController(RuntimeAnimatorController newController);
    }
}