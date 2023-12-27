using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Reloading : ShootingBaseState<ShootingState>
    {
        public Reloading(ShootingState key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            equipment.OnCurrentWeaponReloadingStarted += () => _stateMachine.TransitToState(ShootingState.Reloading);
            equipment.OnCurrentWeaponReloadingFinished += ToShooting;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _heroAnimator.PlayReload();
        }
    }
}