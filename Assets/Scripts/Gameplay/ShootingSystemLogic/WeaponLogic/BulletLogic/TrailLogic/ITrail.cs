using System;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic.TrailLogic
{
    public interface ITrail
    {
        event Action OnFinished;
        void Initialize();
        void Show();
        void Hide();
    }
}