using UnityEngine;

namespace Gameplay.UnitLogic.ZombieLogic.AnimationLogic
{
    public class ZombieAnimator  : MonoBehaviour, IZombieAnimator
    {
        [SerializeField] private Animator _animator;
        
        public void Prepare()
        {
            _animator.enabled = true;
        }

        public void Stop()
        {
            _animator.enabled = false;
        }
    }
}