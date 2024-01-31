using System;
using Gameplay.MatchLogic;
using Gameplay.MatchLogic.SpawnLogic;
using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic.PlayerInputLogic
{
    public class PlayerGameplayInputHandler : IPlayerGameplayInputHandler
    {
        private IInputService _inputService;
        private IMatchSystem _matchSystem;
        private ISpawnSystem _spawnSystem;
        private IRespawnSystem _respawnSystem;
        private IEquipment _equipment;

        public event Action OnSwitchingInputReceived;
        public event Action<bool> OnSwitchingInputStatusChanged;
        public event Action OnThrowingInputReceived;
        public event Action<bool> OnThrowingInputStatusChanged;
        public event Action OnReloadingInputReceived;
        public event Action<bool> OnReloadingInputStatusChanged;
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
            _equipment = ServiceLocator.Get<IEquipment>();

            _inputService.OnMovementDeltaReceived += MovementProcessing;
            _inputService.OnRotationDeltaReceived += RotationProcessing;
            _inputService.OnSwitchingInputReceived += SwitchingProcessing;
            _inputService.OnThrowingInputReceived += ThrowingProcessing;
            _inputService.OnReloadingInputReceived += ReloadingProcessing;

            _respawnSystem.OnStarted += () => _inputService.SetActionMapEnableStatus(InputMode.Gameplay, false);
            _respawnSystem.OnFinished += () => _inputService.SetActionMapEnableStatus(InputMode.Gameplay, true);

            _matchSystem.OnStarted +=  () => _inputService.SetActionMapEnableStatus(InputMode.Gameplay, true);
            _matchSystem.OnFinished +=  () => _inputService.SetActionMapEnableStatus(InputMode.Gameplay, false);

            _spawnSystem.OnSpawned += ResetMovementInput;
            
            _equipment.OnCurrentWeaponReloadingStarted += () => SetNotMovementInputLockStatus(true);
            _equipment.OnCurrentWeaponReloadingFinished += () => SetNotMovementInputLockStatus(false);
            _equipment.OnWeaponSwitchingStarted += () => SetNotMovementInputLockStatus(true); 
            _equipment.OnWeaponSwitchingFinished += () => SetNotMovementInputLockStatus(false);
            _equipment.OnGrenadeLaunchingStarted += () => SetNotMovementInputLockStatus(true);
            _equipment.OnGrenadeLaunchingFinished += () => SetNotMovementInputLockStatus(false);
        }

        private void MovementProcessing(Vector2 value)
        {
            MovementDirection = value;
        }

        private void RotationProcessing(Vector2 value)
        {
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

        private void SetNotMovementInputLockStatus(bool status)
        {
            _isLocked = status;
            
            OnSwitchingInputStatusChanged?.Invoke(!status);
            OnThrowingInputStatusChanged?.Invoke(!status);
            OnReloadingInputStatusChanged?.Invoke(!status);
        }
    }
}