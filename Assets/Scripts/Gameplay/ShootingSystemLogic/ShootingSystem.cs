using System;
using Gameplay.ShootingSystemLogic.EquipmentContainerLogic;
using UnityEngine;

namespace Gameplay.ShootingSystemLogic
{
    public class ShootingSystem : MonoBehaviour, IShootingSystem
    {
        private IEquipmentContainer _equipmentContainer;
        
        [SerializeField] private Transform _rightHandContainer;
        [SerializeField] private Transform _leftHandContainer;
        [SerializeField] private Transform _firstWeaponContainer;
        [SerializeField] private Transform _secondWeaponContainer;
        
        private void Awake()
        {
            _equipmentContainer = new EquipmentContainer();
            _equipmentContainer.SetUp(_rightHandContainer,_leftHandContainer,_firstWeaponContainer, _secondWeaponContainer);
        }
    }
}