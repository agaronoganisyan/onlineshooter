using Infrastructure;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class PlayerController: MonoBehaviour, IInitializable
    {
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private Transform _transform;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private IInputService _inputService;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        public void Initialize()
        {
            _inputService = ServiceLocator.Get<IInputService>();
        }

        private void HandleMovement()
        {
            var movementDirection = new Vector3(_inputService.MovementDirection.x, 0, _inputService.MovementDirection.y);
            var adjustedDirection = _transform.rotation * movementDirection;

            if (movementDirection.magnitude > 0)
                _characterController.Move(adjustedDirection * (_moveSpeed * Time.deltaTime));
        }

        private void HandleRotation()
        {
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation,
                Quaternion.Euler(0, _inputService.RotationDirection.x, 0), _rotationSpeed * Time.deltaTime);
        }
    }
}