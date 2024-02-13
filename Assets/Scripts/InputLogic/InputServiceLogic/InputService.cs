using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputLogic.InputServiceLogic
{
    public enum InputMode
    {
        None,
        UI,
        Gameplay
    }
    
    public class InputService : IInputService
    {
        public event Action<Vector2> OnMovementDeltaReceived;
        public event Action<Vector2> OnRotationDeltaReceived;
        public event Action OnSwitchingInputReceived;
        public event Action OnThrowingInputReceived;
        public event Action OnReloadingInputReceived;
        
        private InputMap _inputMap;
        
        public void Initialize()
        {
            _inputMap = new InputMap();

            _inputMap.Gameplay.MovementDelta.performed +=
                (context) => OnMovementDeltaReceived?.Invoke(context.ReadValue<Vector2>());
            _inputMap.Gameplay.RotationDelta.performed += 
                (context) => SetRotationDelta(context.ReadValue<Vector2>());
            _inputMap.Gameplay.Switching.performed += (context) => OnSwitchingInputReceived?.Invoke();
            _inputMap.Gameplay.Throw.performed += (context) => OnThrowingInputReceived?.Invoke();
            _inputMap.Gameplay.Reloading.performed += (context) => OnReloadingInputReceived?.Invoke();
        }
    
        public void SetInputMode(InputMode mode)
        {
            switch (mode)
            {
                case InputMode.Gameplay:
                    _inputMap.Gameplay.Enable();
                    break;
                case InputMode.UI:
                    _inputMap.Gameplay.Disable();
                    break;
            }
        }
        
        public void SetActionMapEnableStatus(InputMode mode, bool status)
        {
            switch (mode)
            {
                case InputMode.Gameplay:
                    if (status) 
                        _inputMap.Gameplay.Enable();
                    else 
                        _inputMap.Gameplay.Disable();
                    break;
            }
        }
        
        public void SetRotationDelta(Vector2 delta)
        {
            OnRotationDeltaReceived?.Invoke(delta);
        }
    }
}
