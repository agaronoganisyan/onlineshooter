using System;
using UnityEngine;

namespace ConfigsLogic
{
    [CreateAssetMenu (fileName = "PlayerConfig", menuName = "Configs/New PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public String Name => _name;
        [SerializeField] private String _name;
    }
}