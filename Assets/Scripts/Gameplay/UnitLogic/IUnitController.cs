using UnityEngine;

namespace Gameplay.UnitLogic
{
    public interface IUnitController
    {
        Transform Transform { get; }
        void Initialize();
        void Prepare(Vector3 position, Quaternion rotation);
        void Tick();
    }
}