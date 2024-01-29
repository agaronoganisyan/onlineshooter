namespace Gameplay.UnitLogic.RagdollLogic
{
    public interface IRagdollHandler
    {
        void Initialize(IUnitHitBox hitBox);
        void Hit();
        void Enable();
        void Disable();
        void Prepare();
    }
}