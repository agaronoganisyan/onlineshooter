using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

public interface IInputService : IService
{
    event Action<Vector2> OnMovementInputReceived;
    event Action<Vector2> OnRotationInputReceived;
    
    Vector2 MovementDirection { get; set; }
    Vector2 RotationDirection { get; set; }
}
