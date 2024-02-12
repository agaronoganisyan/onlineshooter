using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolLogic
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private Action _onPushBackAllObjects;
        
        private T _prefab;
        private Stack<T> _pooledObjects = new Stack<T>();
        
        public void Initialize(T prefab, int initialSize)
        {
            _prefab = prefab;
            
            InitialSpawn(initialSize);
        }

        public T Pull()
        {
            if (_pooledObjects.Count == 0)
            {
                CreateAndAddToPoolNewObject();
            }

            return _pooledObjects.Pop();
        }

        public void Push(T pushedObject)
        {
            if (_pooledObjects.Contains(pushedObject)) return;
            _pooledObjects.Push(pushedObject);
            pushedObject.gameObject.SetActive(false);
        }
        
        public void PushBackAll()
        {
            _onPushBackAllObjects?.Invoke();
        }
        
        private void InitialSpawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CreateAndAddToPoolNewObject();
            }
        }

        private void CreateAndAddToPoolNewObject()
        {
            T createdObject = Object.Instantiate(_prefab).GetComponent<T>();
            _onPushBackAllObjects += createdObject.ReturnToPool;
            createdObject.PoolInitialize(Push);

            _pooledObjects.Push(createdObject);
                
            createdObject.gameObject.SetActive(false);
        }
    }
}