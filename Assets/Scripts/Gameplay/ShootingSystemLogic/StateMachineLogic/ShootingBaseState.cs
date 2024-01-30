using System;
using ConfigsLogic;
using Gameplay.ShootingSystemLogic.AimLogic;
using Gameplay.ShootingSystemLogic.EnemiesDetectorLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.UnitLogic.PlayerLogic.AnimationLogic;
using Infrastructure.StateMachineLogic;
using Infrastructure.StateMachineLogic.Simple;
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
        GrenadeLaunching,
        Stopping
    }
    
    public abstract class ShootingBaseState<State> : ISimpleState<State> where State : Enum
    {
        public State StateKey => _stateKey;
        private readonly State _stateKey;
        
        protected readonly IStateMachine<ShootingState> _stateMachine;
        
        protected readonly IPlayerAnimator _playerAnimator;
        private readonly IEnemiesDetector _enemiesDetector;
        protected readonly IEquipment _equipment;
        protected readonly IEquipmentContainer _equipmentContainer;
        protected IAim _aim;

        protected ShootingSystemConfig _shootingSystemConfig;
        
        protected readonly float _minAngleToStartingShooting;
        
        protected ShootingBaseState(State key, IStateMachine<ShootingState> stateMachine, IPlayerAnimator playerAnimator, IEquipment equipment, IEquipmentContainer equipmentContainer,
            IEnemiesDetector enemiesDetector, IAim aim, ShootingSystemConfig shootingSystemConfig)
        {
            _stateKey = key;

            _stateMachine = stateMachine;

            _playerAnimator = playerAnimator;
            _enemiesDetector = enemiesDetector;
            _equipment = equipment;
            _aim = aim;
            _equipmentContainer = equipmentContainer;
            
            _shootingSystemConfig = shootingSystemConfig;
            
            _minAngleToStartingShooting = _shootingSystemConfig.MinAngleToStartingShooting;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            _aim.Tick();
        }
        
        protected virtual void TryToEnterThisShootingState()
        {
            if (IsCurrentStateIsNonShootingType()) return;
        }
        
        protected virtual void ToShooting()
        {
            if (_enemiesDetector.IsThereTarget()) _stateMachine.TransitToState(ShootingState.Shooting);
            else _stateMachine.TransitToState(ShootingState.Searching);
        }
        
        protected bool IsCurrentStateIsNonShootingType()
        {
            return IsNonShootingState(_stateMachine.GetCurrentState());
        }
        
        protected bool IsNonShootingState(ShootingState state)
        {
            return state.Equals(ShootingState.Reloading) ||
                   state.Equals(ShootingState.Switching) ||
                   state.Equals(ShootingState.GrenadeLaunching)
                ? true
                : false;
        }
    }
}