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
        
        public ObjectPool(T prefab, int initialSize)
        {
            _prefab = prefab;
            Spawn(initialSize);
        }

        public T Pull()
        {
            T pooledObject;
            if (_pooledObjects.Count > 0)
                pooledObject = _pooledObjects.Pop();
            else
            {
                pooledObject = Object.Instantiate(_prefab).GetComponent<T>();   
            }

            //pooledObject.gameObject.SetActive(true);
            pooledObject.PoolInitialize(Push);

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
        
        private void Spawn(int amount)
        {
            T createdObject;
            
            for (int i = 0; i < amount; i++)
            {
                createdObject = Object.Instantiate(_prefab).GetComponent<T>();
                _pooledObjects.Push(createdObject);
                _onPushBackAllObjects += createdObject.ReturnToPool;
                createdObject.gameObject.SetActive(false);
            }
        }
    }
}