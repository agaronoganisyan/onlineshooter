using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.ServiceLogic
{
    public static class ServiceLocator 
    {
        private static readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

        public static void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogWarning($"A service of type is already registered.");
                return;
            }

            _services.Add(key, service);
        }

        public static void Unregister<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogWarning($"No service of type registered.");
                return;
            }

            _services.Remove(key);
        }

        public static void ReRegister<T>(T serviceNewSignature) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                _services[key] = serviceNewSignature;
            }
            else
            {
                Debug.LogWarning($"No service of type registered.");
            }
        }
        
        public static T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"No service of type registered.");
                return default;
            }

            return (T)_services[key];
        }
    }
}