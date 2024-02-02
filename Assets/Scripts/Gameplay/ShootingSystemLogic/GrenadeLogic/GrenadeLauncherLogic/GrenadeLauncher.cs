using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic.BulletLogic;
using Infrastructure;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public abstract class GrenadeLauncher  : MonoBehaviour, IInitializable
    {
        [SerializeField] private GrenadeConfig _grenadeConfig;
        private IGrenadeFactory _grenadeFactory;

        [SerializeField] private Transform _transform;
        
        private Grenade _currentGrenade;
        
        public void Initialize(IEquipmentContainer equipmentContainer)
        {
            _transform.SetParent(equipmentContainer.LeftHandContainer);
            _transform.localPosition = Vector3.zero;
            _transform.localEulerAngles = Vector3.zero;
            
            _grenadeFactory = ServiceLocator.Get<IGrenadeFactory>();
        }

        public void Launch(Vector3 targetPosition)
        {
            _currentGrenade = _grenadeFactory.Get();
            _currentGrenade.Activate(_transform.position, targetPosition,  _grenadeConfig.Damage,_grenadeConfig.ImpactRadius);
        }
    }
}