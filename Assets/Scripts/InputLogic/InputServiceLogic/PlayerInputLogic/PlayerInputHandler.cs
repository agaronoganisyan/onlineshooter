using System;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic.PlayerInputLogic
{
    public class PlayerInputHandler : IPlayerInputHandler
    {
        private IInputService _inputService;
        private IMatchSystem _matchSystem;
        private ISpawnSystem _spawnSystem;
        private IRespawnSystem _respawnSystem;
        
        public event Action OnSwitchingInputReceived;
        public event Action OnThrowingInputReceived;
        public event Action OnReloadingInputReceived;
        public Vector2 MovementDirection { get; private set; }
        public Vector2 RotationDirection { get; private set; }
        
        private Vector2 _cachedRotation;

        private bool _isLocked;
        
        public void Initialize()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();
            _spawnSystem = ServiceLocator.Get<ISpawnSystem>();
            
            _inputService.OnMovementDeltaReceived += MovementProcessing;
            _inputService.OnRotationDeltaReceived += RotationProcessing;
            _inputService.OnSwitchingInputReceived += SwitchingProcessing;
            _inputService.OnThrowingInputReceived += ThrowingProcessing;
            _inputService.OnReloadingInputReceived += ReloadingProcessing;

            _respawnSystem.OnStarted += () => SetInputLockStatus(true);
            _respawnSystem.OnFinished += () => SetInputLockStatus(false);

            _matchSystem.OnStarted +=  () => SetInputLockStatus(false);
            _matchSystem.OnFinished +=  () => SetInputLockStatus(true);

            _spawnSystem.OnSpawned += ResetMovementInput;
        }

        private void MovementProcessing(Vector2 value)
        {
            if (_isLocked) return;

            MovementDirection = value;
        }

        private void RotationProcessing(Vector2 value)
        {
            if (_isLocked) return;
            
            _cachedRotation += value;
            RotationDirection = _cachedRotation;
        }

        private void SwitchingProcessing()
        {
            if (_isLocked) return;
            OnSwitchingInputReceived?.Invoke();
        }

        private void ThrowingProcessing()
        {
            if (_isLocked) return;
            OnThrowingInputReceived?.Invoke();
        }

        private void ReloadingProcessing()
        {
            if (_isLocked) return;
            OnReloadingInputReceived?.Invoke();
        }

        private void ResetMovementInput(SpawnPointInfo info)
        {
            _cachedRotation = Vector2.zero;
            RotationDirection = _cachedRotation;
            MovementDirection = Vector2.zero;
        }

        private void SetInputLockStatus(bool status)
        {
            _isLocked = status;
        }
    }
}