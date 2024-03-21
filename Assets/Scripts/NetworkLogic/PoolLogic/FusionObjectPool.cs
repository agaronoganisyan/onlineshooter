using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace NetworkLogic.PoolLogic
{
    public class FusionObjectPool
    {
        public NetworkObject Prefab => _prefab;
        private NetworkObject _prefab;
        
        private List<NetworkObject> _pool = new List<NetworkObject>();

        public FusionObjectPool(NetworkObject prefab)
        {
            _prefab = prefab;
        }

        public NetworkObject GetFromPool()
        {
            if (!IsThereAvailableObject())
            {
                CreateAndAddToPoolNewObject();
            }

            return GetAvailableObject();
        }

        public void Cleanup()
        {
            int poolCount = _pool.Count;
            for (int i=0; i< poolCount; i++) Object.Destroy(_pool[i].gameObject);
            
            _pool = new List<NetworkObject>();
        }

        public bool IsContain(NetworkObject networkObject)
        {
            return _pool.Contains(networkObject);
        }

        private NetworkObject GetAvailableObject()
        {
            int pooledObjectsCount = _pool.Count;
            for (int i = 0; i < pooledObjectsCount; i++)
            {
                if (!_pool[i].gameObject.activeSelf) return _pool[i];
            }
            
            Debug.LogError("No available prefab");
            return null;
        }

        private bool IsThereAvailableObject()
        {
            int pooledObjectsCount = _pool.Count;
            for (int i = 0; i < pooledObjectsCount; i++)
            {
                if (!_pool[i].gameObject.activeSelf) return true;
            }

            return false;
        }

        private void CreateAndAddToPoolNewObject()
        {
            NetworkObject createdObject = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            if (createdObject.TryGetComponent(out INetworkPoolable target))
            {
                target.PoolInitialize();
            }
            
            _pool.Add(createdObject);
                
            createdObject.gameObject.SetActive(false);
        }
    }
}