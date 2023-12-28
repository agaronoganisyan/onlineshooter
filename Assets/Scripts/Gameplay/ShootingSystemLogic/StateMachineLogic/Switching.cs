using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Switching : ShootingBaseState<ShootingState>
    {
        public Switching(ShootingState key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            equipment.OnWeaponSwitchingStarted += () => _stateMachine.TransitToState(ShootingState.Switching);;
            heroAnimator.AnimationEventHandler.OnSwitchWeapon += Switch;
            heroAnimator.AnimationEventHandler.OnSwitchingFinished += ToShooting;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _heroAnimator.PlayDraw();
            
            if (_equipment.CurrentWeapon.IsReloading) _equipment.CurrentWeapon.StopReloading();
        }
        
        private void Switch()
        {
            _equipment.SwitchWeapon();

            _equipment.CurrentWeapon.Draw();
            _equipment.NextWeapon.LayDown();

            _heroAnimator.SetRuntimeAnimatorController(_equipment.CurrentWeapon.WeaponConfig.AnimatorOverride);
        }
    }
}