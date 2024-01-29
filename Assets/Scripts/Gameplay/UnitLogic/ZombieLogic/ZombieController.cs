using UnityEngine;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class ZombieController : MonoBehaviour, IUnitController
    {
        private CharacterController _characterController;

        public Transform Transform => _transform;
        private Transform _transform;

        public void Initialize()
        {
            _characterController = GetComponent<CharacterController>();
            _transform = transform;
        }

        public void Prepare(Vector3 position, Quaternion rotation)
        {            
            _characterController.enabled = true;
        }

        public void Tick()
        {
        }

        public void Stop()
        {
            _characterController.enabled = false;
        }
    }
}