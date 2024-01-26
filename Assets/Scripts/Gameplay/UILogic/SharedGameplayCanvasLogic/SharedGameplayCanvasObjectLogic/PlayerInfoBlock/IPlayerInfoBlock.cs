using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.UnitLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public interface IPlayerInfoBlock
    {
        void Initialize(UnitInfo info, PlayerInfoBlockConfig playerInfoBlockConfig, Camera worldCamera, bool isTeammate);
        void Show();
        void Hide();
        void Cleanup();
        void SetParent(Transform parent);
        void Tick();
        Transform TargetHead { get; }
    }
}