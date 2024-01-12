using UnityEngine;

namespace HelpersLogic
{
    public struct DetectionOnScreenFunctions
    {
        public static Vector2 GetScreenPosition(Camera mainCamera, Vector3 targetPosition)
        {
            Vector2 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);
            return screenPosition;
        }
        
        public static bool IsScreenTargetInsideScreenBorders(Vector3 screenPosition) 
        {
            bool status = screenPosition.x > 0 && 
                          screenPosition.x < Screen.width && 
                          screenPosition.y > 0 && 
                          screenPosition.y < Screen.height;

            return status;
        }
        
        public static bool IsWorldTargetInsideScreenBorders(Camera mainCamera, Vector3 targetPosition)
        {
            Vector2 screenPosition = GetScreenPosition(mainCamera, targetPosition);
            
            bool status = screenPosition.x > 0 && 
                          screenPosition.x < Screen.width && 
                          screenPosition.y > 0 && 
                          screenPosition.y < Screen.height;

            return status;
        }
        
        public static bool IsTargetVisible(Vector3 targetPosition, Vector3 cameraPosition, LayerMask obstacleLayerMask)
        {
            bool isThereObstacle = Physics.Linecast(targetPosition, cameraPosition, obstacleLayerMask);

            return !isThereObstacle;
        }
    }
}