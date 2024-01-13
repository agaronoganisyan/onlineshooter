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
            IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer,
            IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig,  GrenadeLaunchingConfig grenadeLaunchingConfig,
            Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
            _grenadeLaunchingConfig = grenadeLaunchingConfig;
            
            equipment.OnGrenadeLaunchingStarted += () => _stateMachine.TransitToState(ShootingState.GrenadeLaunching);;
            heroAnimator.AnimationEventHandler.OnThrow += Throw;
            heroAnimator.AnimationEventHandler.OnThrowingFinished += ToShooting;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _heroAnimator.PlayThrow();
            
            if (_equipment.CurrentWeapon.IsReloading) _equipment.CurrentWeapon.StopReloading();
        }
        
        private void Throw()
        {
            _equipment.CurrentGrenadeLauncher.Launch(_target.position, _grenadeLaunchingConfig);
        }
    }
}