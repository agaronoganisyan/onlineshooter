using System;
using ConfigsLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyLogic.ControlsSetupLogic
{
    public class SensitivitySetup : MonoBehaviour
    {
        private ControlsSettingsConfig _controlsSettings;
        
        [SerializeField] private Slider _slider;

        [SerializeField] private TextMeshProUGUI _value;
        
        private void Awake()
        {
            Initialize();
        }

        private  void Initialize()
        {
            _controlsSettings = ServiceLocator.Get<ControlsSettingsConfig>();

            _slider.minValue = _controlsSettings.SensitivityMinValue;
            _slider.maxValue = _controlsSettings.SensitivityMaxValue;
            _slider.onValueChanged.AddListener(x => { 
                _controlsSettings.SetSensitivity(x);
                _value.text = Math.Round(x,2).ToString();
            });
            
            _slider.value = _controlsSettings.GetSensitivity();
        }
    }
}