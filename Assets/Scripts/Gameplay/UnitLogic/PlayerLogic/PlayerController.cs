using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class PlayerController: MonoBehaviour, IPlayerController
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private Transform _transform;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private IInputService _inputService;

        public void Initialize()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            _heroAnimator.PlayIdle();
        }

        public void Tick()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            var movementDirection = new Vector3(_inputService.MovementDirection.x, 0, _inputService.MovementDirection.y);
            var adjustedDirection = _transform.rotation * movementDirection;

            if (movementDirection.magnitude > 0)
                _characterController.Move(adjustedDirection * (_moveSpeed * Time.deltaTime));
            
            _heroAnimator.PlayMovement(_inputService.MovementDirection);
        }

        private void HandleRotation()
        {
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation,
                Quaternion.Euler(0, _inputService.RotationDirection.x, 0), _rotationSpeed * Time.deltaTime);
        }
    }
}