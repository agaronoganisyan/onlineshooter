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
            T pooledObject = _pooledObjects.Count > 0 ? _pooledObjects.Pop() : CreateNew();
            
            return pooledObject;
        }

        public void Push(T pushedObject)
        {
            _pooledObjects.Push(pushedObject);
            pushedObject.gameObject.SetActive(false);
        }
        
        public void PushBackAll()
        {
            _onPushBackAllObjects?.Invoke();
        }
        
        private void InitialSpawn(int amount)
        {
            T createdObject;
            
            for (int i = 0; i < amount; i++)
            {
                createdObject = Object.Instantiate(_prefab).GetComponent<T>();
                _onPushBackAllObjects += createdObject.ReturnToPool;
                createdObject.PoolInitialize(Push);
                createdObject.gameObject.SetActive(false);
                
                _pooledObjects.Push(createdObject);
            }
        }

        private T CreateNew()
        {
            T createdObject;
            
            createdObject = Object.Instantiate(_prefab).GetComponent<T>();
            _onPushBackAllObjects += createdObject.ReturnToPool;
            createdObject.PoolInitialize(Push);
            createdObject.gameObject.SetActive(false);

            return createdObject;
        }
    }
}