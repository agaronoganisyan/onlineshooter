using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Shooting : ShootingBaseState<ShootingState>
    {
        public Shooting(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            enemiesDetector.OnEnemyDetected += (T) => TryToEnterThisShootingState();
        }
        
        public override void Enter()
        {
            base.Enter();
            PlayerAnimator.PlayAim();
            
            if (_equipment.CurrentWeapon.IsRequiredReloading()) _equipment.CurrentWeapon.StartReloading();
        }

        public override void Update() 
        {
            base.Update();
            
            if (!_equipment.CurrentWeapon.IsReadyToFire(_target,_minAngleToStartingShooting)) return;
            _equipment.CurrentWeapon.Fire();
        }
        
        protected override void TryToEnterThisShootingState()
        {
            base.TryToEnterThisShootingState();
            _stateMachine.TransitToState(ShootingState.Shooting);
        }
    }
}