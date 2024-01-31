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
        private IPlayerGameplayInputHandler gameplayInputService;
        
        [SerializeField] private Transform _cameraObjectTransform;
        [SerializeField] private Transform _transform;
        private Transform _targetTransform;
        
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;
        private float _startYRotation;

        public void Initialize()
        {
            _targetTransform = ServiceLocator.Get<Player>().Transform;
            gameplayInputService = ServiceLocator.Get<IPlayerGameplayInputHandler>();
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
        
        private void LateUpdate()
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
            _transform.localEulerAngles = new Vector3(0,  _startYRotation + gameplayInputService.RotationDirection.x, 0);
        }

        private void HandleMovement()
        {
            _transform.position = _targetTransform.position;
        }
    }
}