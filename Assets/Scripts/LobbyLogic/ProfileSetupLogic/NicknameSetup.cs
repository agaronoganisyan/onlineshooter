using ConfigsLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;

namespace LobbyLogic.ProfileSetupLogic
{
    public class NicknameSetup : MonoBehaviour
    {
        private ProfileSettingsConfig _profileSettings;
        
        [SerializeField] private TMP_InputField _inputField;

        private void Awake()
        {
            Initialize();
        }

        private  void Initialize()
        {
            _profileSettings = ServiceLocator.Get<ProfileSettingsConfig>();

            _inputField.onValueChanged.AddListener(x => _profileSettings.SetNickname(x));
            
            _inputField.text = _profileSettings.GetNickname();
        }
    }
}