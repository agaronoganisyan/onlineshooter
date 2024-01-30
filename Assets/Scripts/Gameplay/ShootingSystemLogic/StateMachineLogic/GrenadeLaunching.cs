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
    public class GrenadeLaunching : ShootingBaseState<ShootingState>
    {
        private GrenadeLaunchingConfig _grenadeLaunchingConfig;

        public GrenadeLaunching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig, GrenadeLaunchingConfig grenadeLaunchingConfig) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            _grenadeLaunchingConfig = grenadeLaunchingConfig;
            
            equipment.OnGrenadeLaunchingStarted += () => _stateMachine.TransitToState(ShootingState.GrenadeLaunching);;
            playerAnimator.AnimationEventHandler.OnThrow += Throw;
            playerAnimator.AnimationEventHandler.OnThrowingFinished += ToShooting;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.PlayThrow();
            
            if (_equipment.CurrentWeapon.IsReloading) _equipment.CurrentWeapon.StopReloading();
        }

        private void Throw()
        {
            _equipment.CurrentGrenadeLauncher.Launch(_aim.Transform.position, _grenadeLaunchingConfig);
        }

        protected override void ToShooting()
        {
            _equipment.FinishGrenadeLaunching();
            base.ToShooting();
        }
    }
}