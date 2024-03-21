using ConfigsLogic;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using Gameplay.UnitLogic;
using Gameplay.UnitLogic.DamageLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic
{
    public class GrenadeLauncher : MonoBehaviour
    {
        private Unit _unit;
        
        [SerializeField] private GrenadeConfig _grenadeConfig;
        private IGrenadeFactory _grenadeFactory;

        [SerializeField] private Transform _transform;

        private Grenade _currentGrenade;
        
        public void Initialize(Unit unit, IEquipmentContainer equipmentContainer)
        {
            _unit = unit;
            
            _transform.SetParent(equipmentContainer.LeftHandContainer);
            _transform.localPosition = Vector3.zero;
            _transform.localEulerAngles = Vector3.zero;

            _grenadeFactory = ServiceLocator.Get<IGrenadeFactory>();
        }

        public void Launch(Vector3 targetPosition)
        {
            _currentGrenade = _grenadeFactory.Get(new HitInfo(_unit.Info.TeamType, _grenadeConfig.Damage),
                _transform.position, targetPosition,_grenadeConfig.ImpactRadius);
        }
    }
}