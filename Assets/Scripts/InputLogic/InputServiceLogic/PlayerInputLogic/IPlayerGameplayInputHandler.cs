using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic.PlayerInputLogic
{
    public interface IPlayerGameplayInputHandler : IService
    {
        event Action OnSwitchingInputReceived;
        event Action<bool> OnSwitchingInputStatusChanged;
        event Action OnThrowingInputReceived;
        event Action<bool> OnThrowingInputStatusChanged;
        event Action OnReloadingInputReceived;
        event Action<bool> OnReloadingInputStatusChanged;
        Vector2 MovementDirection { get; }
        Vector2 RotationDirection { get; }
        void Initialize();
    }
}