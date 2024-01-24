using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.CameraLogic
{
    public interface IGameplayCamera : IService
    {
        Camera Camera { get; }
        Transform Transform { get; }
        void Initialize();
        void Enable();
        void Disable();
    }
}