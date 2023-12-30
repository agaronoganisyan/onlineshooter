using UnityEngine;

namespace Gameplay.CameraLogic
{
    public class CameraHitBox : MonoBehaviour, IShakable
    {
        [SerializeField] private CameraController _cameraController;
        public void Shake()
        {
            _cameraController.Shake();
        }
    }
}