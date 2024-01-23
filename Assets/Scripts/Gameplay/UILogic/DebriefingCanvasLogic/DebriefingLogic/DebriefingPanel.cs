using Gameplay.MatchLogic;
using Gameplay.MatchLogic.PointsLogic;
using Infrastructure.CanvasPanelBase;
using Infrastructure.GameStateMachineLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic.DebriefingCanvasLogic.DebriefingLogic
{
    public class DebriefingPanel : PanelBase, IDebriefingPanel
    {
        private IGameStateMachine _gameStateMachine;
        private IMatchSystem _matchSystem;
        private IPointsSystem _pointsSystem;
        
        [SerializeField] private Image _panel;
        
        [SerializeField] private TextMeshProUGUI _resultText;

        [SerializeField] private Color _victoryColor;
        [SerializeField] private Color _defeatColor;
        [SerializeField] private Color _drawColor;
        
        private string _victoryText = "Victory";
        private string _defeatText = "Defeat";
        private string _drawText = "Draw";

        public override void Initialize()
        {
            _gameStateMachine = ServiceLocator.Get<IGameStateMachine>();
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _pointsSystem = ServiceLocator.Get<IPointsSystem>();
            
            _matchSystem.OnFinished += () => Show(true);
            _pointsSystem.OnMatchResultDetermined += PointsSystemOnOnMatchResultDetermined;
            
            base.Initialize();
        }

        public void OnLobby()
        {
            _gameStateMachine.SwitchState(GameState.Lobby);
        }

        private void PointsSystemOnOnMatchResultDetermined(MatchResultType resultType)
        {
            switch (resultType)
            {
                case MatchResultType.Victory:
                    Prepare(_victoryColor, _victoryText);
                    break;
                case MatchResultType.Defeat:
                    Prepare(_defeatColor, _defeatText);
                    break;
                case MatchResultType.Draw:
                    Prepare(_drawColor, _drawText);
                    break;
            }
        }

        private void Prepare(Color color, string text)
        {
            _panel.color = color;
            _resultText.text = text;
        }
    }
}