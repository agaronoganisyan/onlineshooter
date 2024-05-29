using System;
using Infrastructure.ServiceLogic;

namespace Infrastructure.ApplicationLogic
{
    public interface IApplicationStateHandler : IService
    {
        event Action OnApplicationClosed;
        void Initialize();
    }
}