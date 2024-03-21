using System;
using ConfigsLogic;
using Gameplay.CameraLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.HealthBarLogic;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;
using PoolLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock
{
    public class PlayerInfoBlock : SharedGameplayCanvasObject, IPlayerInfoBlock, IPoolable<PlayerInfoBlock>
    {
        private Action<PlayerInfoBlock> _returnToPool;
        
        private ISharedGameplayCanvasSystem _canvasSystem;
        private PlayerInfoBlockConfig _config;
        private Camera _worldCamera;
        
        [SerializeField] private HealthBar _healthBar;
        
        [SerializeField] private TextMeshProUGUI _nameText;

        public Transform TargetHead => _unit.HeadTransform;

        public void Prepare(Unit unit , bool isTeammate)
        {
            base.Prepare(unit, _config.OffsetToTarget, _worldCamera);
            _healthBar.Prepare(unit.GetHealthSystem(),
                isTeammate
                    ? _config.TeammateFirstColor
                    : _config.EnemyFirstColor,
                isTeammate
                    ? _config.TeammateSecondColor
                    : _config.EnemySecondColor);

            _nameText.text = unit.Info.Name;
            _nameText.color = isTeammate
                ? _config.TeammateFirstColor
                : _config.EnemyFirstColor;
            
            Enable();
            
            _unit.OnDied += Remove;
        }

        public void Cleanup()
        {
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

        private void Remove()
        {
            _canvasSystem.RemoveUnitInfoObject(this);
        }

        private void OnDisable()
        {
            ReturnToPool();
        }
        
        #region POOL_LOGIC

        public void PoolInitialize(Action<PlayerInfoBlock> returnAction)
        {
            _returnToPool = returnAction; 
            
            _canvasSystem = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            _config = ServiceLocator.Get<PlayerInfoBlockConfig>();
            _worldCamera = ServiceLocator.Get<IGameplayCamera>().Camera;
        }

        public void ReturnToPool()
        {
            if (_unit != null)
            {
                _unit.OnDied -= Remove;
                
                _healthBar.Cleanup();
                SetParent(null);   
            }
            
            gameObject.SetActive(false);
            _returnToPool?.Invoke(this);
        }

        #endregion
    }
}
