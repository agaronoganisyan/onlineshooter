using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class GrenadeLaunching : ShootingBaseState<ShootingState>
    {
        private GrenadeLaunchingConfig _grenadeLaunchingConfig;

        public GrenadeLaunching(ShootingState key, IStateMachine<ShootingState> stateMachine,
            IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer,
            IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig,  GrenadeLaunchingConfig grenadeLaunchingConfig,
            Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            _grenadeLaunchingConfig = grenadeLaunchingConfig;
            
            equipment.OnGrenadeLaunchingStarted += () => _stateMachine.TransitToState(ShootingState.GrenadeLaunching);;
            playerAnimator.AnimationEventHandler.OnThrow += Throw;
            playerAnimator.AnimationEventHandler.OnThrowingFinished += ToShooting;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            PlayerAnimator.PlayThrow();
            
            if (_equipment.CurrentWeapon.IsReloading) _equipment.CurrentWeapon.StopReloading();
        }
        
        private void Throw()
        {
            _equipment.CurrentGrenadeLauncher.Launch(_target.position, _grenadeLaunchingConfig);
        }
        
        protected override void ToShooting()
        {
            _equipment.FinishGrenadeLaunching();
            base.ToShooting();
        }
    }
}