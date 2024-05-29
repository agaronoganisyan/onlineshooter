using System;
using UnityEngine;

namespace Infrastructure.ApplicationLogic
{
    public class ApplicationStateHandler : MonoBehaviour, IApplicationStateHandler
    {
        public event Action OnApplicationClosed;

        public void Initialize()
        {
            
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            
        }

        private void OnApplicationQuit()
        {
            OnApplicationClosed?.Invoke();
        }
        
        
    }
}