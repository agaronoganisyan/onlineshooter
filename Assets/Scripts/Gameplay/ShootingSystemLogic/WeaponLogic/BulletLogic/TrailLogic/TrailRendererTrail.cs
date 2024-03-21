using System;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic.TrailLogic
{
    public class TrailRendererTrail : MonoBehaviour, ITrail
    {
        public event Action OnFinished;

        private TrailRenderer _trailRenderer;

        public void Initialize()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            Hide();
        }

        public void Show()
        {
            _trailRenderer.Clear();
            gameObject.SetActive(true);
            _trailRenderer.enabled = true;
        }

        public void Hide()
        {
            _trailRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }
}