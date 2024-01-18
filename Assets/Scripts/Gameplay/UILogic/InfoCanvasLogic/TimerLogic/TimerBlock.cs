using Gameplay.MatchLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.TimerLogic
{
    public class TimerBlock : MonoBehaviour, ITimerBlock
    {
        [SerializeField] private TextMeshProUGUI _valueText;

        private IMatchSystem _matchSystem;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _matchSystem = ServiceLocator.Get<IMatchSystem>();
            _matchSystem.OnMatchTimeGiven += SetValue;
            _matchSystem.OnRestOfMatchTimeChanged += SetValue;
        }

        private void SetValue(string value)
        {
            _valueText.text = value;
        }
    }
}