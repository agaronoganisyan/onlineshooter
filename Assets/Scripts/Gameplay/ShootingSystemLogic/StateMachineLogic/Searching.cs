using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Searching : ShootingBaseState<ShootingState>
    {
        public Searching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            enemiesDetector.OnNoEnemyDetected += TryToEnterThisShootingState;
        }

        public override void Enter()
        {
            base.Enter();
            _playerAnimator.PlayIdle();
            
            if (_equipment.CurrentWeapon.IsRequiredReloading()) _equipment.CurrentWeapon.StartReloading();
        }

        protected override void TryToEnterThisShootingState()
        {
            base.TryToEnterThisShootingState();
            _stateMachine.TransitToState(ShootingState.Searching);
        }
    }
}