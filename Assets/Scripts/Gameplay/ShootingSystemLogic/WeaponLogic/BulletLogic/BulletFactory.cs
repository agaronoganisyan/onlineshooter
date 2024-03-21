using ConfigsLogic;
using Fusion;
using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic
{
    public class BulletFactory : IBulletFactory
    {
        private INetworkManager _networkManager;

        private BulletFactoryConfig _factoryConfig;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _factoryConfig = ServiceLocator.Get<BulletFactoryConfig>();

            //_pool = new ObjectPool<Bullet>();
            //_pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
        
        public Bullet Get()
        {
            return _networkManager.NetworkRunner.Spawn(_factoryConfig.Prefab);
        }

        public Bullet Get(HitInfo hitInfo, Vector3 startPosition, Vector3 direction, float speed, float lifeTime)
        {
            var key = new NetworkObjectPredictionKey {Byte0 = (byte) _networkManager.NetworkRunner.LocalPlayer.RawEncoded, Byte1 = (byte) _networkManager.NetworkRunner.Simulation.Tick};

            return _networkManager.NetworkRunner.Spawn(_factoryConfig.Prefab, startPosition, Quaternion.LookRotation(direction), _networkManager.NetworkRunner.LocalPlayer, 
                (runner, obj ) =>
                {
                    obj.GetComponent<Bullet>().InitNetworkState(hitInfo, startPosition, direction, speed, lifeTime);
                },key);
        }

        public void ReturnAllObjectToPool()
        {
            
        }
    }
}