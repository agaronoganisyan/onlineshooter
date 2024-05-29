using Cysharp.Threading.Tasks;
using Gameplay.ShootingSystemLogic.GrenadeLogic.GrenadeLauncherLogic;
using Gameplay.ShootingSystemLogic.WeaponLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.ServiceLogic;
using NetworkLogic;
using NetworkLogic.MatchLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentFactoryLogic
{
    public class EquipmentFactory : IEquipmentFactory
    {
        private INetworkManager _networkManager;
        private IAssetsProvider _assetsProvider;
        
        public void Initialize()
        {
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async UniTask<Weapon> GetWeapon(string address, INetworkMatchHandler networkMatchHandler)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(address);
            Weapon obj = _networkManager.NetworkRunner.Spawn(prefab).GetComponent<Weapon>();
            networkMatchHandler.RPC_AddPlayerObject(_networkManager.NetworkRunner.LocalPlayer, obj.Object);

            return obj;
        }

        public async UniTask<GrenadeLauncher> GetGrenadeLauncher(string address)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(address);
            GrenadeLauncher obj = Object.Instantiate(prefab).GetComponent<GrenadeLauncher>();
            return obj;
        }

        public void UnloadEquipment(string equipmentAddress)
        {
            _assetsProvider.Unload(equipmentAddress);
        }
    }
}