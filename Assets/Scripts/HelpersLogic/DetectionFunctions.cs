using UnityEngine;

namespace HelpersLogic
{
    public struct DetectionFunctions
    {
        public static bool IsWithinAngle(Vector3 originPosition, Vector3 originForwardVector, Vector3 targetPosition, float angle)
        {
            originPosition.y = 0;
            targetPosition.y = 0;
            originForwardVector.y = 0;
            originForwardVector.Normalize();
            
            float angleBetween = Vector3.Angle(originForwardVector, targetPosition - originPosition);
            return Mathf.Abs(angleBetween) < angle / 2;
        }
    }
}