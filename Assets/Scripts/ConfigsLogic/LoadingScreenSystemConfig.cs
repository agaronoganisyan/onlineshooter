using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "LoadingScreenSystemConfig", menuName = "Configs/New LoadingScreenSystemConfig")]
    public class LoadingScreenSystemConfig : ScriptableObject, IService
    {
        public float ShowingDuration => _showingDuration;
        [SerializeField] private float _showingDuration;
    }
}