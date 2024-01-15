namespace Gameplay.ShootingSystemLogic
{
    public interface IShootingSystem
    {
        void Initialize();
        void Tick();
        void Prepare();
    }
}