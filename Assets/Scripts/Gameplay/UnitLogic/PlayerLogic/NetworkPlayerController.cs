using Fusion;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class NetworkPlayerController : NetworkBehaviour, IUnitController
    {
        private PlayerAnimator _playerAnimator;
        private IPlayerGameplayInputHandler _gameplayInputService;

        private CharacterController _characterController;

        public Transform Transform => _transform;
        private Transform _transform;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private float _startYRotation;

        public void Initialize()
        {
            _playerAnimator = GetComponentInChildren<PlayerAnimator>();
            _characterController = GetComponent<CharacterController>();
            _gameplayInputService = ServiceLocator.Get<IPlayerGameplayInputHandler>();

            _transform = transform;
        }

        public void Prepare(Vector3 position, Quaternion rotation)
        {
            _characterController.enabled = true;
            
            _transform.position = position;
            _transform.rotation = rotation;

            _startYRotation = rotation.eulerAngles.y;
        }

        public void Stop()
        {
            _characterController.enabled = false;
        }

        public override void FixedUpdateNetwork()
        {
            if (HasStateAuthority == false) return;
            
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            Vector3 movementDirection = new Vector3(_gameplayInputService.MovementDirection.x, 0, _gameplayInputService.MovementDirection.y);
            Vector3 adjustedDirection = _transform.rotation * movementDirection;

            if (movementDirection.magnitude > 0)
                _characterController.Move(adjustedDirection * (_moveSpeed * Runner.DeltaTime));

            _playerAnimator.PlayMovement(_gameplayInputService.MovementDirection);
        }

        private void HandleRotation()
        {
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation,
                Quaternion.Euler(0, _startYRotation + _gameplayInputService.RotationDirection.x, 0), _rotationSpeed * Runner.DeltaTime);
        }
    }
}