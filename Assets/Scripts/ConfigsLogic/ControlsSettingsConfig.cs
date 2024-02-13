using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu(fileName = "ControlsSettingsConfig", menuName = "Configs/New ControlsSettingsConfig")]
    public class ControlsSettingsConfig : ScriptableObject, IService
    {
        [SerializeField] private float _sensitivity;
        public float SensitivityMinValue => _sensitivityMinValue;
        [SerializeField] private float _sensitivityMinValue;
        public float SensitivityMaxValue => _sensitivityMaxValue;
        [SerializeField] private float _sensitivityMaxValue;
        public float GetSensitivity()
        {
            if (_sensitivity <= 0) return _sensitivity = _sensitivityMinValue;
            else return _sensitivity;
        }

        public void SetSensitivity(float value)
        {
            if (value <= 0) _sensitivity = _sensitivityMinValue;
            else _sensitivity = value;
        }
    }
}