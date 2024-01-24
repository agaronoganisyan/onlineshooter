using DG.Tweening;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public class CameraController : MonoBehaviour , ICameraController
    {
        private IPlayerInputHandler _inputService;
        private ISpawnSystem _spawnSystem;

        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
        
        public Transform CameraObjectTransform => _cameraObjectTransform;
        [SerializeField] private Transform _cameraObjectTransform;
        [SerializeField] private Transform _transform;
        private Transform _targetTransform;
        
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;
        private float _startYRotation;

        public void Initialize()
        {
            _targetTransform = ServiceLocator.Get<Player>().Transform;
            _inputService = ServiceLocator.Get<IPlayerInputHandler>();
            
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            _spawnSystem.OnSpawned += Prepare;
        }

        public void Prepare(SpawnPointInfo spawnPointInfo)
        {
            _transform.position = spawnPointInfo.Position;
            _transform.rotation = spawnPointInfo.Rotation;

            _startYRotation = spawnPointInfo.Rotation.eulerAngles.y;
        }
        
        public void SetTarget(Transform target)
        {
            _transform = target;
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
            _transform.localEulerAngles = new Vector3(0,  _startYRotation + _inputService.RotationDirection.x, 0);
        }

        private void HandleMovement()
        {
            _transform.position = _targetTransform.position;
        }
    }
}