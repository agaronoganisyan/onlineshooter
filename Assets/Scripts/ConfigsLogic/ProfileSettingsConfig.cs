using System;
using Infrastructure.ServiceLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "ProfileSettingsConfig", menuName = "Configs/New ProfileSettingsConfig")]
    public class ProfileSettingsConfig : ScriptableObject, IService
    {
        [SerializeField] private string _nickname;
        
        public string GetNickname()
        {
            if (_nickname.Length == 0) return _nickname = "Player_" + Random.Range(1,999999);
            else return _nickname;
        }

        public void SetNickname(string value)
        {
            _nickname = value;
        }
    }
}