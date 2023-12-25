using UnityEngine;

namespace Gameplay.ShootingSystemLogic.EquipmentContainerLogic
{
    public interface IEquipmentContainer
    {
        Transform RightHandContainer { get; }
        Transform LeftHandContainer { get; }
        Transform FirstWeaponContainer { get; }
        Transform SecondWeaponContainer { get; }

        void SetUp(Transform rightHandContainer, Transform leftHandContainer, Transform firstWeaponContainer,
            Transform secondWeaponContainer);
    }
}