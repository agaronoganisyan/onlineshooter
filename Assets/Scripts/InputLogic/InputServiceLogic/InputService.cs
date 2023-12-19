using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    public event Action<Vector2> OnMovementInputReceived;
    public event Action<Vector2> OnRotationInputReceived;
    public Vector2 MovementDirection { get; set; }
    public Vector2 RotationDirection { get; set; }
    private InputMap _inputMap;
    private Vector2 _cachedRotation;
    public void Initialize()
    {
        _inputMap = new InputMap();
        _inputMap.Gameplay.Enable();
        _inputMap.Gameplay.MovementDelta.performed += (context) => MovementDirection = context.ReadValue<Vector2>();
        _inputMap.Gameplay.RotationDelta.performed += ProcessRotation;
    }
    
    private void ProcessRotation(InputAction.CallbackContext context)
    {
        _cachedRotation += context.ReadValue<Vector2>();
        RotationDirection = _cachedRotation;
        OnRotationInputReceived?.Invoke(RotationDirection);
    }
}