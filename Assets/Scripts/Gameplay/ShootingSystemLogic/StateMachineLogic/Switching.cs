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
    public class Switching : ShootingBaseState<ShootingState>
    {
        public Switching(ShootingState key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig) : base(key, stateMachine, playerAnimator, equipment, equipmentContainer, enemiesDetector, aim, shootingSystemConfig)
        {
            equipment.OnWeaponSwitchingStarted += () => _stateMachine.TransitToState(ShootingState.Switching);;
            playerAnimator.AnimationEventHandler.OnSwitchWeapon += Switch;
            playerAnimator.AnimationEventHandler.OnSwitchingFinished += ToShooting;
        }

        public override void Enter()
        {
            base.Enter();
            
            _playerAnimator.PlayDraw();
            
            if (_equipment.CurrentWeapon.IsReloading) _equipment.CurrentWeapon.StopReloading();
        }

        private void Switch()
        {
            _equipment.SwitchWeapon();

            _equipment.CurrentWeapon.Draw();
            _equipment.NextWeapon.LayDown();

            _playerAnimator.SetRuntimeAnimatorController(_equipment.CurrentWeapon.WeaponConfig.AnimatorOverride);
        }

        protected override void ToShooting()
        {
            _equipment.FinishWeaponSwitching();
            base.ToShooting();
        }
    }
}