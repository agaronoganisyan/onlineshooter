using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public class Initializing : ShootingBaseState<ShootingState>
    {
        public Initializing(ShootingState key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer, IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed) : base(key, stateMachine, heroAnimator, equipment, equipmentContainer, enemiesDetector, shootingSystemConfig, crosshair, crosshairBasePosition, crosshairMovementSpeed)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Init();
        }

        void Init()
        {
            _equipment.CurrentWeapon.Initialize(_equipmentContainer.RightHandContainer,
                _equipment.CurrentWeapon.WeaponConfig.WeaponType == WeaponType.First ?
                    _equipmentContainer.FirstWeaponContainer : _equipmentContainer.SecondWeaponContainer);
            
            _equipment.NextWeapon.Initialize(_equipmentContainer.RightHandContainer,
                _equipment.NextWeapon.WeaponConfig.WeaponType == WeaponType.First ?
                    _equipmentContainer.FirstWeaponContainer : _equipmentContainer.SecondWeaponContainer);
            
            _equipment.CurrentGrenadeLauncher.Initialize(_equipmentContainer.LeftHandContainer);
            
            _equipment.CurrentWeapon.Draw();
            _equipment.NextWeapon.LayDown();
            
            _stateMachine.TransitToState(ShootingState.Searching);
        }
    }
}