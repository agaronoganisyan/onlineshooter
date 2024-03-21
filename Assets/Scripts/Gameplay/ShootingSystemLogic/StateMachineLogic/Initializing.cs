using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Initializing : ShootingBaseState<ShootingState>
    {
        public Initializing(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, Unit unit, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, unit, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Init();
        }

        void Init()
        {
            _equipment.Prepare(_unit, _equipmentContainer);
                
            _stateMachine.TransitToState(ShootingState.Searching);
        }
    }
}