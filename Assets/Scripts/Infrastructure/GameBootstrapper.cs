using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
            InitServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IInputService>( new InputService());
        }

        private void InitServices()
        {
            ServiceLocator.Get<IInputService>().Initialize();
        }
    }
}