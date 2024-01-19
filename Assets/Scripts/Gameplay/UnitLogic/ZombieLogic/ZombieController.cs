using UnityEngine;

namespace Gameplay.UnitLogic.ZombieLogic
{
    public class ZombieController : MonoBehaviour, IUnitController
    {
        public Transform Transform { get; }
        public void Initialize()
        {
            
        }

        public void Prepare(Vector3 position, Quaternion rotation)
        {
        }

        public void Tick()
        {
        }
    }
}