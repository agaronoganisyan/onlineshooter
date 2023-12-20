using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public class HeroAnimator : MonoBehaviour, IHeroAnimator
    {
        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _aimHash = Animator.StringToHash("Aim");

        private readonly int _movementVerticalHash = Animator.StringToHash("MovementVertical");
        private readonly int _movementHorizontalHash = Animator.StringToHash("MovementHorizontal");

        [SerializeField] private Animator _animator;
        
        private Vector2 _movementDirection;
        
        private float _movementEasingSpeed = 7.5f;
        private float _transitionDuration = .4f;

        public void PlayIdle()
        {
            _animator.CrossFade(_idleHash, _transitionDuration,1);
        }

        public void PlayAim()
        {
            _animator.CrossFade(_aimHash, _transitionDuration,1);

        }

        public void PlayMovement(Vector2 movementDirection)
        {
            _movementDirection =
                Vector2.Lerp(_movementDirection, movementDirection, _movementEasingSpeed * Time.deltaTime);
            
            _animator.SetFloat(_movementHorizontalHash, _movementDirection.x);
            _animator.SetFloat(_movementVerticalHash, _movementDirection.y);
        }
    }
}