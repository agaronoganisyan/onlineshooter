using Gameplay.MatchLogic.SpawnLogic.RespawnLogic;
using Infrastructure.CanvasPanelBase;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.DebriefingCanvasLogic.RespawnLogic
{
    public class RespawnPanel : PanelBase, IRespawnPanel
    {
        private string _respawnTimerText = "RESPAWN IN: ";
        
        private IRespawnSystem _respawnSystem;
        
        [SerializeField] private TextMeshProUGUI _timerText;
        
        public override void Initialize()
        {
            _respawnSystem = ServiceLocator.Get<IRespawnSystem>();
            _respawnSystem.OnStarted += () => Show(true);
            _respawnSystem.OnFinished += () =>  Hide(true);
            _respawnSystem.OnStopped += () =>  Hide(true);
            _respawnSystem.OnRespawnTimeGiven += UpdateTimer;
            _respawnSystem.OnRestOfRespawnTimeChanged += UpdateTimer;
            
            base.Initialize();
        }

        private void UpdateTimer(string value)
        {
            _timerText.text = _respawnTimerText + value;
        }
    }
}