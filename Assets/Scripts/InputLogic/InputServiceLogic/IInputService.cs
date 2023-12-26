using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic
{
    public interface IInputService : IService
    {
        event Action<Vector2> OnMovementInputReceived;
        event Action<Vector2> OnRotationInputReceived;
        event Action OnSwitchingInputReceived;
        event Action OnThrowingInputReceived;
        event Action OnReloadingInputReceived;
    
        Vector2 MovementDirection { get; set; }
        Vector2 RotationDirection { get; set; }
    
        void Initialize();
    }
}
