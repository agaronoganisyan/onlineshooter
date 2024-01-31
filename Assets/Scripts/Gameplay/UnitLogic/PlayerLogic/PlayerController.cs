using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.UnitLogic.PlayerLogic
{
    public class PlayerController: MonoBehaviour, IUnitController
    {
        [FormerlySerializedAs("_heroAnimator")] [SerializeField] private PlayerAnimator playerAnimator;
        private IPlayerGameplayInputHandler gameplayInputService;

        [SerializeField] private CharacterController _characterController;

        public Transform Transform => _transform;
        [SerializeField] private Transform _transform;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private float _startYRotation;

        public void Initialize()
        {
            gameplayInputService = ServiceLocator.Get<IPlayerGameplayInputHandler>();
            playerAnimator.PlayIdle();
        }

        public void Prepare(Vector3 position, Quaternion rotation)
        {
            _characterController.enabled = true;
            
            _transform.position = position;
            _transform.rotation = rotation;

            _startYRotation = rotation.eulerAngles.y;
            
        }
        public void Tick()
        {
            HandleMovement();
            HandleRotation();
        }

        public void Stop()
        {
            _characterController.enabled = false;
        }

        private void HandleMovement()
        {
            var movementDirection = new Vector3(gameplayInputService.MovementDirection.x, 0, gameplayInputService.MovementDirection.y);
            var adjustedDirection = _transform.rotation * movementDirection;

            if (movementDirection.magnitude > 0)
                _characterController.Move(adjustedDirection * (_moveSpeed * Time.deltaTime));
            
            playerAnimator.PlayMovement(gameplayInputService.MovementDirection);
        }

        private void HandleRotation()
        {
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation,
                Quaternion.Euler(0, _startYRotation + gameplayInputService.RotationDirection.x, 0), _rotationSpeed * Time.deltaTime);
        }
    }
}