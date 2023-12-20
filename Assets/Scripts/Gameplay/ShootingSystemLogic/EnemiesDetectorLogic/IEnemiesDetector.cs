using System;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EnemiesDetectorLogic
{
    public interface IEnemiesDetector
    {
        event Action<Transform> OnEnemyDetected;
        event Action OnNoEnemyDetected;
        void Start();
        void Stop();
        bool IsThereTarget();
    }
}