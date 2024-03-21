using System;
using Gameplay.HealthLogic;
using Gameplay.UnitLogic.DamageLogic;
using UnityEngine;

namespace Gameplay.UnitLogic
{
    public interface IUnitHitBox : IDamageable
    {
        event Action<Vector3> OnHitTaken;
        void Initialize(Unit unit);
        void Prepare();
    }
}