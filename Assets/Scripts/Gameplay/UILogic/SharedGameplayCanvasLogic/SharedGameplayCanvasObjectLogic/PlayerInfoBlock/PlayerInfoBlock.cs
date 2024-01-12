using ConfigsLogic;
using Gameplay.HealthLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public class PlayerInfoBlock : SharedGameplayCanvasObject, IPlayerInfoBlock
    {
        [SerializeField] private HealthBar _healthBar;
        
        [SerializeField] private TextMeshProUGUI _nameText;

        public Transform TargetHead => _targetHeadTransform;
        
        public void Initialize(PlayerConfig playerConfig, PlayerInfoBlockConfig playerInfoBlockConfig, HealthSystem healthSystem,
            Transform target, Transform targetHead, Camera worldCamera)
        {
            base.Initialize(target, targetHead, playerInfoBlockConfig.OffsetToTarget, worldCamera);
            _healthBar.Initialize(healthSystem);

            _nameText.text = playerConfig.Name;
            gameObject.SetActive(true);
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void Disable()
        {
            base.Disable();
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