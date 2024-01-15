using System;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "PlayerConfig", menuName = "Configs/New PlayerConfig")]
    public class PlayerConfig : ScriptableObject, IService
    {
        public String Name => _name;
        [SerializeField] private String _name;
    }
}