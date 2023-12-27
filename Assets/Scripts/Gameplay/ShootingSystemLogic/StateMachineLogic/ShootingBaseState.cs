using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.StateMachineLogic
{
    public enum ShootingState
    {
        None,
        Initializing,
        Searching,
        Shooting,
        Reloading,
        Switching,
        GrenadeLaunching
    }
    
    public abstract class ShootingBaseState<State> : IState<State> where State : Enum
    {
        public State StateKey => _stateKey;
        private readonly State _stateKey;
        
        protected readonly IStateMachine<ShootingState> _stateMachine;
        
        protected readonly IHeroAnimator _heroAnimator;
        private readonly IEnemiesDetector _enemiesDetector;
        protected readonly IEquipment _equipment;
        protected readonly IEquipmentContainer _equipmentContainer;

        private readonly Transform _crosshair;
        private readonly Transform _crosshairBasePosition;
        protected Transform _target;
        
        private readonly Vector3 _offsetForTargetShooting;
        
        private readonly float _crosshairMovementSpeed;
        protected readonly float _minAngleToStartingShooting;
        
        protected ShootingBaseState(State key, IStateMachine<ShootingState> stateMachine, IHeroAnimator heroAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer,
            IEnemiesDetector enemiesDetector, ShootingSystemConfig shootingSystemConfig, Transform crosshair, Transform crosshairBasePosition, float crosshairMovementSpeed)
        {
            _stateKey = key;

            _stateMachine = stateMachine;

            _heroAnimator = heroAnimator;
            _enemiesDetector = enemiesDetector;
            _equipment = equipment;
            _equipmentContainer = equipmentContainer;
            
            _crosshair = crosshair;
            _crosshairBasePosition = crosshairBasePosition;
            _offsetForTargetShooting = shootingSystemConfig.OffsetForTargetShooting;
            _crosshairMovementSpeed = crosshairMovementSpeed;
            _minAngleToStartingShooting = shootingSystemConfig.MinAngleToStartingShooting;
            
            _enemiesDetector.OnEnemyDetected += AimToTarget;
            _enemiesDetector.OnNoEnemyDetected += SetCrosshairBasePosition;
            
            SetCrosshairBasePosition();
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            _crosshair.position = Vector3.MoveTowards(_crosshair.position, _target.position + _offsetForTargetShooting,
                _crosshairMovementSpeed * Time.deltaTime);
        }
        
        protected void ToShooting()
        {
            if (_enemiesDetector.IsThereTarget()) _stateMachine.TransitToState(ShootingState.Shooting);
            else _stateMachine.TransitToState(ShootingState.Searching);
        }
        
        private void AimToTarget(Transform target)
        {
            _target = target;
        }

        private void SetCrosshairBasePosition()
        {
            _target = _crosshairBasePosition;
        }
    }
}