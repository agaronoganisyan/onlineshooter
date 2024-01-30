using System;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.AimLogic
{
    public interface IAim
    {
        event Action<Vector3> OnAimPositionChanged;
        Transform Transform { get; }
        void Initialize(IEnemiesDetector enemiesDetector);
        void Tick();
    }
}