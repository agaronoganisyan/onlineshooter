using UnityEngine;

namespace Gameplay.CameraLogic.ControllerLogic
{
    public interface ICameraController 
    {
        void Initialize();
        void Prepare(Vector3 position, Quaternion rotation);
        void SetTarget(Transform target);
        void Enable();
        void Disable();
    }
}