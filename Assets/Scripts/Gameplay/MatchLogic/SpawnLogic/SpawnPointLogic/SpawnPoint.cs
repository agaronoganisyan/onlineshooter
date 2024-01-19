using UnityEngine;

namespace Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic
{
    public class SpawnPoint : MonoBehaviour
    {
        public Transform Transform => _transform;
        [SerializeField] private Transform _transform;
    }
}