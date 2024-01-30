using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.AimLogic
{
    public interface IAimSystem : IService
    {
        event Action<Vector3> OnAimPositionChanged;
        void Initialize(IAim aim);
    }
}