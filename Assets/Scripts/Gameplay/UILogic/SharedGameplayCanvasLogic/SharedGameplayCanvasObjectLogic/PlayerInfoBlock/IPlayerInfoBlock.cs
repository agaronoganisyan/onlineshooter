using ConfigsLogic;
using Gameplay.HealthLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public interface IPlayerInfoBlock
    {
        void Initialize(PlayerConfig playerConfig, PlayerInfoBlockConfig playerInfoBlockConfig,
            HealthSystem healthSystem,
            Transform target, Transform targetHead, Camera worldCamera);
        void Enable();
        void Disable();
        void SetParent(Transform parent);
        void Tick();
        Transform TargetHead { get; }
    }
}