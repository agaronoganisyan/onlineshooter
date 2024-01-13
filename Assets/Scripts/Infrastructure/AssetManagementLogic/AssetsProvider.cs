using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.AssetManagementLogic
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssets = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
             Addressables.InitializeAsync();
        }

        public async UniTask<T> Load<T>(string address) where T: class
        {
            if (_cachedAssets.TryGetValue(address, out AsyncOperationHandle cachedHandle))
                return cachedHandle.Result as T;
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            handle.Completed += operationHandle =>
            {
                _cachedAssets[address] = operationHandle;
            };

            AddHandle(address, handle);

            return await handle.Task.AsUniTask();
        }

        public void Unload(string address)
        {
            List<AsyncOperationHandle> handles = GetHandlesForKey(address);
            for (int i=0; i < handles.Count; i++) Addressables.Release(handles[i]);
            _cachedAssets.Remove(address);
            _handles.Remove(address);
        }

        public async UniTask<SceneInstance> LoadScene(AssetReference sceneReference)
        {
            AsyncOperationHandle<SceneInstance> handle = sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            return await handle.Task.AsUniTask();
        }
        
        public async UniTask UnloadScene(SceneInstance sceneReference)
        {
            await Addressables.UnloadSceneAsync(sceneReference);
        }
        
        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles  in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
                Addressables.Release(handle);
            
            _cachedAssets.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string path, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(path, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[path] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
        
        private List<AsyncOperationHandle> GetHandlesForKey(string key)
        {
            if (_handles.TryGetValue(key, out List<AsyncOperationHandle> handlesList))
            {
                return handlesList;
            }
            else
            {
                Debug.LogWarning($"Key '{key}' not found in the dictionary.");
                return new List<AsyncOperationHandle>();
            }
        }
    }
}