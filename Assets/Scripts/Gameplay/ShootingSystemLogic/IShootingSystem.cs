namespace Gameplay.ShootingSystemLogic
{
    public interface IShootingSystem
    {
        void Initialize();
        void Prepare();
        void Stop();
        void FixedTick();
    }
}