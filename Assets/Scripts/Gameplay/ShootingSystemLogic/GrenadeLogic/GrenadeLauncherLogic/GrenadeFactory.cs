using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic;
using PoolLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public class GrenadeFactory : IGrenadeFactory
    {
        private INetworkManager _networkManager;

        private GrenadeFactoryConfig _factoryConfig;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _factoryConfig = ServiceLocator.Get<GrenadeFactoryConfig>();

            //_pool = new ObjectPool<Grenade>();
            //_pool.Initialize(_factoryConfig.Prefab, _factoryConfig.InitialPoolSize);
        }
        
        public Grenade Get()
        {
            return _networkManager.NetworkRunner.Spawn(_factoryConfig.Prefab);
        }

        public Grenade Get(HitInfo hitInfo, Vector3 startPosition, Vector3 targetPosition, float impactRadius)
        {
            return _networkManager.NetworkRunner.Spawn(_factoryConfig.Prefab, startPosition, Quaternion.identity, _networkManager.NetworkRunner.LocalPlayer, 
                (runner, obj ) =>
                {
                    obj.GetComponent<Grenade>().InitNetworkState(hitInfo, startPosition, targetPosition, impactRadius);
                });
        }
        
        public void ReturnAllObjectToPool()
        {
            
        }
    }
}