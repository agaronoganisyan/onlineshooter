using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace NetworkLogic.PoolLogic
{
    public class NetworkObjectPoolSystem : INetworkObjectPoolSystem
    {
        private List<FusionObjectPool> _pools = new List<FusionObjectPool>();
        
        public void Initialize()
        {
            
        }

        public NetworkObject AcquireInstance(NetworkRunner runner, NetworkPrefabInfo info)
        {
            if (NetworkProjectConfig.Global.PrefabTable.TryGetPrefab(info.Prefab, out NetworkObject prefab))
            {
                NetworkObject newt = GetObj(prefab);

                newt.gameObject.SetActive(true);
                return newt;
            }

            Debug.LogError("No prefab for " + info.Prefab);
            return null;
        }

        public void ReleaseInstance(NetworkRunner runner, NetworkObject networkObject, bool isSceneObject)
        {
            ReturnToPool(networkObject);
        }

        public void ClearPools()
        {
            int poolsCount = _pools.Count;
            for (int i = 0; i < poolsCount; i++)
            {
                _pools[i].Cleanup();
            }

            _pools = new();
        }

        private NetworkObject GetObj(NetworkObject prefab)
        {
            int poolsCount = _pools.Count;
            for (int i = 0; i < poolsCount; i++)
            {
                if (_pools[i].Prefab == prefab)
                {
                    return _pools[i].GetFromPool();
                }
            }

            FusionObjectPool newPool = new FusionObjectPool(prefab); 
            _pools.Add(newPool);
            return newPool.GetFromPool();
        }

        private void ReturnToPool(NetworkObject networkObject)
        {
            networkObject.gameObject.SetActive(false);

            if (!IsPoolContainObject(networkObject)) Object.Destroy(networkObject.gameObject);
        }

        private bool IsPoolContainObject(NetworkObject networkObject)
        {
            int poolsCount = _pools.Count;
            for (int i = 0; i < poolsCount; i++)
            {
                if (_pools[i].IsContain(networkObject)) return true;
            }

            return false;
        }
    }
}