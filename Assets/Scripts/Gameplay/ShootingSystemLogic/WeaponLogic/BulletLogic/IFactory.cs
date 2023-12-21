namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public interface IFactory<T>
    {
        T Get();
    }
}