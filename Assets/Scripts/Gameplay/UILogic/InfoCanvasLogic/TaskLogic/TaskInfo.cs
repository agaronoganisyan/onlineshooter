using Gameplay.MatchLogic.TaskLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.TaskLogic
{
    public class TaskInfo : MonoBehaviour
    {
        private IMatchTaskSystem _matchTaskSystem;
        
        [SerializeField] private TextMeshProUGUI _text;
        
        private void Awake()
        {
            Initialize();
        }

        private  void Initialize()
        {
            _matchTaskSystem = ServiceLocator.Get<IMatchTaskSystem>();
            _matchTaskSystem.OnTaskChanged += SetTask;
        }

        private void SetTask(string task)
        {
            _text.text = task;
        }
    }
}