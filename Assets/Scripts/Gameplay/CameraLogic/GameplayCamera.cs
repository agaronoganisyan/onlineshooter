using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public class GameplayCamera : MonoBehaviour, IGameplayCamera
    {
        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
        
        public Transform Transform  => transform;
        [SerializeField] private Transform transform;

        private ICameraController _cameraController;
        private ISpawnSystem _spawnSystem;
        
        public void Initialize()
        {
            _cameraController = GetComponent<ICameraController>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            
            _cameraController.Initialize();
            
            _spawnSystem.OnSpawned += Prepare;
            
            Disable();
        }

        private void Prepare(SpawnPointInfo spawnPointInfo)
        {
            _cameraController.Prepare(spawnPointInfo.Position, spawnPointInfo.Rotation);
            Enable();
        }

        public void Enable()
        {
            enabled = true;
            //_cameraController.Enable();
        }
        
        public void Disable()
        {
            enabled = false;
            _cameraController.Disable();
        }
    }
}