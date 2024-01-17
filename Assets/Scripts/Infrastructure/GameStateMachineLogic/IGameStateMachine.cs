using Infrastructure.ServiceLogic;

namespace Infrastructure.GameStateMachineLogic
{
    public interface IGameStateMachine : IService
    {
        void Initialize();
        void SwitchState(GameState gameState);

    }
}