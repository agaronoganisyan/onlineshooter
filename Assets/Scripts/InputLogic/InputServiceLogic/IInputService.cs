using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace InputLogic.InputServiceLogic
{
    public interface IInputService : IService
    {
        event Action<Vector2> OnMovementDeltaReceived;
        event Action<Vector2> OnRotationDeltaReceived;
        event Action OnSwitchingInputReceived;
        event Action OnThrowingInputReceived;
        event Action OnReloadingInputReceived;
        void Initialize();
        void SetInputMode(InputMode mode);
        void SetActionMapEnableStatus(InputMode mode, bool status);
    }
}
