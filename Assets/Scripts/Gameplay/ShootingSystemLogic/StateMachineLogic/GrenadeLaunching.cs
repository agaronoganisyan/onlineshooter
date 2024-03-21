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
    public class GrenadeLaunching : ShootingBaseState<ShootingState>
    {
        public GrenadeLaunching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerGameplayInputHandler gameplayInputHandler, Unit unit, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, gameplayInputHandler, unit, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            gameplayInputHandler.OnThrowingInputReceived += () => _stateMachine.TransitToState(ShootingState.GrenadeLaunching);
            playerAnimator.AnimationEventHandler.OnThrow += Throw;
            playerAnimator.AnimationEventHandler.OnThrowingFinished += ToShooting;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.PlayThrow();
            _equipment.StartGrenadeLaunching();
        }

        public override void Exit()
        {
            base.Exit();
            
            _equipment.FinishGrenadeLaunching();
        }
        
        private void Throw()
        {
            _equipment.LaunchGrenade(_aim.Transform.position);
        }

        protected override void ToShooting()
        {
            _equipment.FinishGrenadeLaunching();
            base.ToShooting();
        }
    }
}