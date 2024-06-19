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
            if (_size >= ArrayLenght) throw new ArgumentOutOfRangeException();
            
            _objects.Set(_size, objectId);
            _size++;
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