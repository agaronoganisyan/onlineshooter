using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using InputLogic.InputServiceLogic.PlayerInputLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Reloading : ShootingBaseState<ShootingState>
    {
        public Reloading(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            gameplayInputHandler.OnReloadingInputReceived += () => _stateMachine.TransitToState(ShootingState.Reloading);
            equipment.OnCurrentWeaponReloadingRequired += () => _stateMachine.TransitToState(ShootingState.Reloading);
            playerAnimator.AnimationEventHandler.OnReloadingFinished += ToShooting;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.PlayReload();
            _equipment.StartWeaponReloading();
        }
        
        public override void Exit()
        {
            base.Exit();
            
            _equipment.FinishWeaponReloading();
        }
        
        protected override void ToShooting()
        {
            _equipment.ReloadWeapon();
            _equipment.FinishWeaponReloading();
            base.ToShooting();
        }
    }
}