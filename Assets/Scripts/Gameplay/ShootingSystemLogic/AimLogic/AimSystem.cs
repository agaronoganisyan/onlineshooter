using System;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.AimLogic
{
    public class AimSystem : IAimSystem
    {
        public event Action<Vector3> OnAimPositionChanged;
        
        private IAim _aim;
        
        public void Initialize(IAim aim)
        {
            _aim = aim;
            _aim.OnAimPositionChanged += (position) => OnAimPositionChanged?.Invoke(position);
        }
        
    }
}