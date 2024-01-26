using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic;
using Gameplay.UnitLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public class PlayerInfoBlock : SharedGameplayCanvasObject, IPlayerInfoBlock
    {
        [SerializeField] private HealthBar _healthBar;
        
        [SerializeField] private TextMeshProUGUI _nameText;

        public Transform TargetHead => _targetHeadTransform;
        
        public void Initialize(UnitInfo info, PlayerInfoBlockConfig playerInfoBlockConfig, Camera worldCamera, bool isTeammate)
        {
            base.Initialize(info.Target, info.TargetHead, playerInfoBlockConfig.OffsetToTarget, worldCamera);
            _healthBar.Initialize(info.HealthSystem,
                isTeammate
                    ? playerInfoBlockConfig.TeammateFirstColor
                    : playerInfoBlockConfig.EnemyFirstColor,
                isTeammate
                    ? playerInfoBlockConfig.TeammateSecondColor
                    : playerInfoBlockConfig.EnemySecondColor);
            
            _nameText.text = info.Name;
            _nameText.color = isTeammate
                ? playerInfoBlockConfig.TeammateFirstColor
                : playerInfoBlockConfig.EnemyFirstColor;

            Enable();
        }

        public void Cleanup()
        {
            _healthBar.Cleanup();
            Disable();
        }
        
        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
        
        public override void SetParent(Transform parent)
        {
            base.SetParent(parent);
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}