using System;
using Fusion;
using UnityEngine;

namespace NetworkLogic.PlayerNetworkObjectsDataLogic
{
    public struct PlayerNetworkObjectsData : INetworkStruct
    {
        private const int ArrayLenght = 5;
        [Networked][Capacity(ArrayLenght)] private NetworkArray<NetworkId> _objects { get; }
        
        private int _size;

        public void AddObject(NetworkId objectId)
        {
            Debug.LogError($"_size {_size} ArrayLenght {ArrayLenght}");
            
            if (_size >= ArrayLenght) throw new ArgumentOutOfRangeException();

            Debug.LogError($"AddObject NetworkId {objectId}");

            _objects.Set(_size, objectId);
            _size++;
            
            Debug.LogError($"_objects.Count {_size}");
            Debug.LogError($"_objects.Count {_size}");
            Debug.LogError($"_objects.Count {_size}");
        }

        public NetworkId GetObjectId(int index)
        {
             return _objects.Get(index);
        }
        
        public int Count()
        {
            return _size;
        }
    }
}