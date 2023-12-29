using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Shooting : ShootingBaseState<ShootingState>
    {
        public Shooting(ShootingState key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            enemiesDetector.OnEnemyDetected += (T) => TryToEnterThisShootingState();
        }
        
        public override void Enter()
        {
            base.Enter();
            _heroAnimator.PlayAim();
            
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