using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public interface ICameraController : IService
    {
        Camera Camera { get; }
        Transform CameraObjectTransform { get; }
        void Initialize();
        void SetTarget(Transform target);
    }
}