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
    public class Shooting : ShootingBaseState<ShootingState>
    {
        public Shooting(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            enemiesDetector.OnEnemyDetected += (T) => TryToEnterThisShootingState();
        }

        public override void Enter()
        {
            base.Enter();
            _playerAnimator.PlayAim();
            
            if (_equipment.CurrentWeapon.IsRequiredReloading()) _equipment.CurrentWeapon.StartReloading();
        }

        public override void Update() 
        {
            base.Update();
            
            if (!_equipment.CurrentWeapon.IsReadyToFire(_aim.Transform,_minAngleToStartingShooting)) return;
            _equipment.CurrentWeapon.Fire();
        }

        protected override void TryToEnterThisShootingState()
        {
            base.TryToEnterThisShootingState();
            _stateMachine.TransitToState(ShootingState.Shooting);
        }
    }
}