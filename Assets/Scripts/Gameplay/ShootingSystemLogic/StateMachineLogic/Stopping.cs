using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Stopping : ShootingBaseState<ShootingState>
    {
        public Stopping(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, Unit unit, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, unit, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
        }
    }
}