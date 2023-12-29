using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Searching : ShootingBaseState<ShootingState>
    {
        public Searching(ShootingState key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            enemiesDetector.OnNoEnemyDetected += TryToEnterThisShootingState;
        }
        
        public override void Enter()
        {
            base.Enter();
            _heroAnimator.PlayIdle();
            
            if (_equipment.CurrentWeapon.IsRequiredReloading()) _equipment.CurrentWeapon.StartReloading();
        }
        
        protected override void TryToEnterThisShootingState()
        {
            base.TryToEnterThisShootingState();
            _stateMachine.TransitToState(ShootingState.Searching);
        }
    }
}