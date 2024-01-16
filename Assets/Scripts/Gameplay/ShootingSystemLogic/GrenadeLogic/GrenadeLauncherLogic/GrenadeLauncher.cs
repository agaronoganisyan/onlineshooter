using ConfigsLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public abstract class GrenadeLauncher  : MonoBehaviour, IInitializable
    {
        [SerializeField] private GrenadeConfig _grenadeConfig;
        private IFactory<Grenade> _grenadeFactory;

        [SerializeField] private Transform _transform;
        
        private Grenade _currentGrenade;
        
        public void Initialize(Transform activeContainer)
        {
            _transform.SetParent(activeContainer);
            _transform.localPosition = Vector3.zero;
            _transform.localEulerAngles = Vector3.zero;
            
            _grenadeFactory = new GrenadeFactory(_grenadeConfig.Prefab, _grenadeConfig.InitialPoolSize);
        }

        public void Launch(Vector3 targetPosition, GrenadeLaunchingConfig grenadeLaunchingConfig)
        {
            _currentGrenade = _grenadeFactory.Get();
            _currentGrenade.Activate(_transform.position, targetPosition, grenadeLaunchingConfig, _grenadeConfig);
        }
    }
}