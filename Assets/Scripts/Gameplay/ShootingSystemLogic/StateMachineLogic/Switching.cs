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
    public class Switching : ShootingBaseState<ShootingState>
    {
        public Switching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            gameplayInputHandler.OnSwitchingInputReceived +=  () => _stateMachine.TransitToState(ShootingState.Switching);
            playerAnimator.AnimationEventHandler.OnSwitchWeapon += Switch;
            playerAnimator.AnimationEventHandler.OnSwitchingFinished += ToShooting;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.PlayDrawFirstPart();
            _equipment.StartWeaponSwitching();
        }

        public override void Exit()
        {
            base.Exit();
            
            _equipment.FinishWeaponSwitching();
        }
        
        private void Switch()
        {
            _equipment.SwitchWeapon();
            
            _playerAnimator.SetRuntimeAnimatorController(_equipment.CurrentWeaponInfo.AnimatorOverride);
            _playerAnimator.PlayDrawSecondPart();
        }

        protected override void ToShooting()
        {
            _equipment.FinishWeaponSwitching();
            base.ToShooting();
        }
    }
}