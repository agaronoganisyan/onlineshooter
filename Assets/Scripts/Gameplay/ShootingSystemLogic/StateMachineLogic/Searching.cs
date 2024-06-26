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
    public class Searching : ShootingBaseState<ShootingState>
    {
        public Searching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, Unit unit, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, unit, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            enemiesDetector.OnNoEnemyDetected += TryToEnterThisShootingState;
        }

        public override void Enter()
        {
            base.Enter();
            _playerAnimator.PlayIdle();
            
            if (_equipment.IsWeaponRequiredReloading()) _stateMachine.TransitToState(ShootingState.Reloading);
        }

        protected override void TryToEnterThisShootingState()
        {
            if (IsCurrentStateIsNonShootingType()) return;
            _stateMachine.TransitToState(ShootingState.Searching);
        }
    }
}