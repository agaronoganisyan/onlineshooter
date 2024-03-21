using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gameplay.UnitLogic.PlayerLogic.AnimationLogic
{
    public class PlayerAnimator : NetworkBehaviour, IPlayerAnimator
    {
        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _aimHash = Animator.StringToHash("Aim");
        private readonly int _reloadHash = Animator.StringToHash("Reload");
        private readonly int _drawFirstPartHash = Animator.StringToHash("DrawFirstPart");
        private readonly int _drawSecondPartHash = Animator.StringToHash("DrawSecondPart");
        private readonly int _throwHash = Animator.StringToHash("Throw");

        private readonly int _movementVerticalHash = Animator.StringToHash("MovementVertical");
        private readonly int _movementHorizontalHash = Animator.StringToHash("MovementHorizontal");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rig _rig;
        [SerializeField] private RigBuilder _rigBuilder;

        public HeroAnimationEventHandler AnimationEventHandler => _heroAnimationEvent;
        [SerializeField] private HeroAnimationEventHandler _heroAnimationEvent;

        private Vector2 _movementDirection;
        
        private float _movementEasingSpeed = 7.5f;
        private float _transitionDuration = .4f;

        public void Prepare()
        {
            _rigBuilder.enabled = true;
            _animator.enabled = true;
        }

        public void Stop()
        {
            _rigBuilder.enabled = false;
            _animator.enabled = false;
        }
        
        public void PlayMovement(Vector2 movementDirection)
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayMovement(movementDirection);
        }
        
        public void PlayIdle()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayIdle();
        }
        
        public void PlayAim()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayAim();
        }
        
        public void PlayReload()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayReload();
        }

        public void PlayDrawFirstPart()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayDrawFirstPart();
        }

        public void PlayDrawSecondPart()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayDrawSecondPart();
        }
        
        public void PlayThrow()
        {
            if (!HasStateAuthority) return;
            
            RPC_PlayThrow();
        }
        
        public void SetRuntimeAnimatorController(RuntimeAnimatorController newController)
        {
            _animator.runtimeAnimatorController = newController;
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayMovement(Vector2 movementDirection)
        {
            _movementDirection =
                Vector2.Lerp(_movementDirection, movementDirection, _movementEasingSpeed * Runner.DeltaTime);
            
            _animator.SetFloat(_movementHorizontalHash, _movementDirection.x);
            _animator.SetFloat(_movementVerticalHash, _movementDirection.y);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayIdle()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration);
            _animator.CrossFade(_idleHash, _transitionDuration,1); 
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayAim()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 1, _transitionDuration);
            _animator.CrossFade(_aimHash, _transitionDuration,1);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayReload()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration);
            _animator.CrossFade(_reloadHash, _transitionDuration,1);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayDrawFirstPart()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration/2); //А НУЖНО ЛИ ВООБЩЕ ЭТО УМЕНЬГЕНИЕ /2???
            _animator.CrossFade(_drawFirstPartHash, _transitionDuration/2,1);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayDrawSecondPart()
        {
            _rig.weight = 0;
            _animator.Play(_drawSecondPartHash);
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayThrow()
        {
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 0, _transitionDuration/2); //А НУЖНО ЛИ ВООБЩЕ ЭТО УМЕНЬГЕНИЕ /2???
            _animator.CrossFade(_throwHash, _transitionDuration/2,1);
        }
    }
}
