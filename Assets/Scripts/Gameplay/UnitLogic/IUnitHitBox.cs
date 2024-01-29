using System;
using Gameplay.HealthLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public interface IUnitHitBox : IDamageable
    {
        event Action<Vector3> OnHitTaken;
        void Initialize(HealthSystem healthSystem);
        void Prepare();
    }
}