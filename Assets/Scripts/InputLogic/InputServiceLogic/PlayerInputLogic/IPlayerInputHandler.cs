using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic.PlayerInputLogic
{
    public interface IPlayerInputHandler : IService
    {
        event Action OnSwitchingInputReceived;
        event Action OnThrowingInputReceived;
        event Action OnReloadingInputReceived;
        Vector2 MovementDirection { get; }
        Vector2 RotationDirection { get; }
        void Initialize();
    }
}