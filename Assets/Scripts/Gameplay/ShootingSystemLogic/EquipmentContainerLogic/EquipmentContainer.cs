using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentContainerLogic
{
    public class EquipmentContainer : IEquipmentContainer
    {
        public Transform RightHandContainer => _rightHandContainer;
        private Transform _rightHandContainer;
        
        public Transform LeftHandContainer => _leftHandContainer;
        private Transform _leftHandContainer;
        
        public Transform FirstWeaponContainer => _firstWeaponContainer;
        private Transform _firstWeaponContainer;
        
        public Transform SecondWeaponContainer=> _secondWeaponContainer;
        private Transform _secondWeaponContainer;

        public void SetUp(Transform rightHandContainer, Transform leftHandContainer, Transform firstWeaponContainer, Transform secondWeaponContainer)
        {
            _rightHandContainer = rightHandContainer;
            _leftHandContainer = leftHandContainer;
            _firstWeaponContainer = firstWeaponContainer;
            _secondWeaponContainer = secondWeaponContainer;
        }
    }
}