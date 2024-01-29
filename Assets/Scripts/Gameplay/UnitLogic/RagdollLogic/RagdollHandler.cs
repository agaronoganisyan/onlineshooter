using UnityEngine;

namespace Gameplay.UnitLogic.RagdollLogic
{
    public class RagdollHandler : MonoBehaviour, IRagdollHandler
    {
        private IUnitHitBox _hitBox;
        
        [SerializeField] private Rigidbody _pelvisRigidbody;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Collider[] _colliders;
        
        private Vector3 _impulseForce;
        
        private float _impulseBaseForce = 250; 
        
        private int _rigidbodiesCount;
        
        public void Initialize(IUnitHitBox hitBox)
        {
            _hitBox = hitBox;
            _hitBox.OnHitTaken += SetLastHitDirection;
            
            _rigidbodiesCount = _rigidbodies.Length;
            
            Disable();
        }

        public void Hit()
        {
            Enable();
            _pelvisRigidbody.AddForce(_impulseForce, ForceMode.Impulse);
        }

        public void Enable()
        {
            SetKinematicStatus(false);
        }

        public void Disable()
        {
            SetKinematicStatus(true);
            ResetRigidbodiesVelocities();
        }

        public void Prepare()
        {
            Disable();
        }

        private void SetKinematicStatus(bool status)
        {
            for (int i = 0; i < _rigidbodiesCount; i++)
            {
                _rigidbodies[i].isKinematic = status;
                _colliders[i].enabled = !status;
            }
        }

        private void ResetRigidbodiesVelocities()
        {
            for (int i = 0; i < _rigidbodiesCount; i++)
            {
                _rigidbodies[i].velocity = Vector3.zero;
                _rigidbodies[i].angularVelocity = Vector3.zero;
            }
        }

        private void SetLastHitDirection(Vector3 direction)
        {
            _impulseForce = direction * _impulseBaseForce;
        }
    }
}