using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public class HeroAnimator : MonoBehaviour, IHeroAnimator
    {
        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _aimHash = Animator.StringToHash("Aim");
        private readonly int _reloadHash = Animator.StringToHash("Reload");
        private readonly int _drawHash = Animator.StringToHash("Draw");
        private readonly int _throwHash = Animator.StringToHash("Throw");

        private readonly int _movementVerticalHash = Animator.StringToHash("MovementVertical");
        private readonly int _movementHorizontalHash = Animator.StringToHash("MovementHorizontal");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rig _rig;
        
        public HeroAnimationEventHandler AnimationEventHandler => _heroAnimationEvent;
        [SerializeField] private HeroAnimationEventHandler _heroAnimationEvent;

        private Vector2 _movementDirection;
        
        private float _movementEasingSpeed = 7.5f;
        private float _transitionDuration = .4f;

        public void Initialize()
        {
        }
        
        public void PlayMovement(Vector2 movementDirection)
        {
            _movementDirection =
                Vector2.Lerp(_movementDirection, movementDirection, _movementEasingSpeed * Time.deltaTime);
            
            _animator.SetFloat(_movementHorizontalHash, _movementDirection.x);
            _animator.SetFloat(_movementVerticalHash, _movementDirection.y);
        }
        
        public void PlayIdle()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration);
            _animator.CrossFade(_idleHash, _transitionDuration,1);
        }
        
        public void PlayAim()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 1, _transitionDuration);
            _animator.CrossFade(_aimHash, _transitionDuration,1);
        }
        
        public void PlayReload()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration);
            _animator.CrossFade(_reloadHash, _transitionDuration,1);
        }

        public void PlayDraw()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration/2); //А НУЖНО ЛИ ВООБЩЕ ЭТО УМЕНЬГЕНИЕ /2???
            _animator.CrossFade(_drawHash, _transitionDuration/2,1);
        }

        public void PlayThrow()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration/2); //А НУЖНО ЛИ ВООБЩЕ ЭТО УМЕНЬГЕНИЕ /2???
            _animator.CrossFade(_throwHash, _transitionDuration/2,1);
        }
        
        public void SetRuntimeAnimatorController(RuntimeAnimatorController newController)
        {
            _animator.runtimeAnimatorController = newController;

        }
    }
}