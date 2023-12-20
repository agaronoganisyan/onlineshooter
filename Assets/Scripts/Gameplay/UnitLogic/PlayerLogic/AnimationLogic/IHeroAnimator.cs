using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public interface IHeroAnimator
    {
        void PlayIdle();
        void PlayAim();
        void PlayMovement(Vector2 movementDirection);
    }
}