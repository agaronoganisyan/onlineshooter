using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentContainerLogic
{
    public class EquipmentContainer : MonoBehaviour, IEquipmentContainer
    {
        public Transform RightHandContainer => _rightHandContainer;
        [SerializeField] private Transform _rightHandContainer;
        
        public Transform LeftHandContainer => _leftHandContainer;
        [SerializeField] private Transform _leftHandContainer;
        
        public Transform FirstWeaponContainer => _firstWeaponContainer;
        [SerializeField]  private Transform _firstWeaponContainer;
        
        public Transform SecondWeaponContainer=> _secondWeaponContainer;
        [SerializeField] private Transform _secondWeaponContainer;

        public void SetUp(Transform rightHandContainer, Transform leftHandContainer, Transform firstWeaponContainer, Transform secondWeaponContainer)
        {
            _rightHandContainer = rightHandContainer;
            _leftHandContainer = leftHandContainer;
            _firstWeaponContainer = firstWeaponContainer;
            _secondWeaponContainer = secondWeaponContainer;
        }
    }
}