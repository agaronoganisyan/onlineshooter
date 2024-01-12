using DG.Tweening;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public class CameraController : MonoBehaviour , ICameraController
    {
        private IInputService _inputService;

        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
        
        public Transform CameraObjectTransform => _cameraObjectTransform;
        [SerializeField] private Transform _cameraObjectTransform;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _targetTransform;
        
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;

        public void Initialize()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            _inputService.OnRotationInputReceived += HandleRotation;
        }

        public void SetTarget(Transform target)
        {
            _transform = target;
        }

        private void Start()
        {
            Initialize();
        }
        
        private void LateUpdate()
        {
            HandleMovement();
        }

        public void Shake()
        {
            _cameraObjectTransform.DOComplete();
            _cameraObjectTransform.DOShakePosition(_shakeDuration, _shakeStrenght);
            _cameraObjectTransform.DOShakeRotation(_shakeDuration, _shakeStrenght);
        }

        void HandleMovement()
        {
            _transform.position = _targetTransform.position;
        }

        void HandleRotation(Vector2 rotation)
        {
            _transform.localEulerAngles = new Vector3(0, rotation.x, 0);
        }
    }
}