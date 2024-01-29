using UnityEngine;

namespace HelpersLogic
{
    public struct Vector3Helper
    {
        public static Vector3 GetDirectionWithoutY(Vector3 startPosition, Vector3 targetPosition)
        {
            startPosition.y = 0;
            targetPosition.y = 0;
            return targetPosition - startPosition;
        }
        
        public static Vector3 GetDirectionWithoutYNormalized(Vector3 startPosition, Vector3 targetPosition)
        {
            startPosition.y = 0;
            targetPosition.y = 0;
            return (targetPosition - startPosition).normalized;
        }
    }
}