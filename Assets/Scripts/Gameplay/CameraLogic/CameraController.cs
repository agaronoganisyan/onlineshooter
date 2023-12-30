using DG.Tweening;
using Infrastructure;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public class CameraController : MonoBehaviour , IInitializable
    {
        private IInputService _inputService;

        [SerializeField] private Transform _cameraObjectTransform;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _targetTransform;
        
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            _inputService.OnRotationInputReceived += HandleRotation;
        }

        private void LateUpdate()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            _transform.position = _targetTransform.position;
        }

        void HandleRotation(Vector2 rotation)
        {
            //_horizontalRotation = delta.x * Time.deltaTime * _rotationSpeed;
            //_horizontalRotation = rotation.x * _rotationSpeed;
            
            _transform.localEulerAngles = new Vector3(0, rotation.x, 0);
            //transform.Rotate(Vector3.up, _horizontalRotation);
        }
        
        public void Shake()
        {
            _cameraObjectTransform.DOComplete();
            _cameraObjectTransform.DOShakePosition(_shakeDuration, _shakeStrenght);
            _cameraObjectTransform.DOShakeRotation(_shakeDuration, _shakeStrenght);
        }
    }
}