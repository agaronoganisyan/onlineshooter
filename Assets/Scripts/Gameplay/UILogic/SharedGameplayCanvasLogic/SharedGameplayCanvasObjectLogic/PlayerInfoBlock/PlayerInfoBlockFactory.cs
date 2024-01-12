using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public class PlayerInfoBlockFactory : ObjectFactory<SharedGameplayCanvasObject>
    {
        public PlayerInfoBlockFactory(SharedGameplayCanvasObject prefab, int initialSize) : base(prefab, initialSize)
        {
        }
    }
}