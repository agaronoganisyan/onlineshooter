using Gameplay.ShootingSystemLogic.EquipmentLogic;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputServiceLogic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] GrenadeLauncher _grenadeLauncher;
        
        private void Awake()
        {
            RegisterServices();
            InitServices();
        }

        private void RegisterServices()
        {
            IInputService inputService = new InputService();
            ServiceLocator.Register<IInputService>(inputService);
            
            ServiceLocator.Register<IEquipment>(new Equipment(inputService));

        }

        private void InitServices()
        {
            IInputService inputService = ServiceLocator.Get<IInputService>();
            inputService.Initialize();

            IEquipment equipment = ServiceLocator.Get<IEquipment>();
            equipment.SetUp(_weapons,_grenadeLauncher);
        }
    }
}