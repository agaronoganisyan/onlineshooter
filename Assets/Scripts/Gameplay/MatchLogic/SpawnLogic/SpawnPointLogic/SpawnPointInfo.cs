using UnityEngine;

namespace Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic
{
    public struct SpawnPointInfo
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public void Setup(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}