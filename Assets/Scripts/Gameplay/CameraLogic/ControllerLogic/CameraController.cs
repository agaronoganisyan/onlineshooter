using DG.Tweening;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.CameraLogic.ControllerLogic
{
    public class CameraController : MonoBehaviour , ICameraController
    {
        private IPlayerGameplayInputHandler _gameplayInputService;
        
        [SerializeField] private Transform _cameraObjectTransform;
        [SerializeField] private Transform _transform;
        private Transform _targetTransform;
        
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;
        private float _startYRotation;
        private float _smoothTime = 0.1f; 
        private float _currentRotationVelocity;
        
        public void Initialize()
        {
            _targetTransform = ServiceLocator.Get<Player>().Transform;
            _gameplayInputService = ServiceLocator.Get<IPlayerGameplayInputHandler>();
        }

        public void Prepare(Vector3 position, Quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;

            _startYRotation = rotation.eulerAngles.y;
        }
        
        public void SetTarget(Transform target)
        {
            _transform = target;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }
        
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        public void Shake()
        {
            _cameraObjectTransform.DOComplete();
            _cameraObjectTransform.DOShakePosition(_shakeDuration, _shakeStrenght);
            _cameraObjectTransform.DOShakeRotation(_shakeDuration, _shakeStrenght);
        }

        private void HandleRotation()
        {
            float smoothedRotation = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _startYRotation + _gameplayInputService.RotationDirection.x, ref _currentRotationVelocity, _smoothTime);
            _transform.rotation = Quaternion.Euler(0, smoothedRotation, 0);
        }

        private void HandleMovement()
        {
            _transform.position = _targetTransform.position;
        }
    }
}