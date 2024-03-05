using Gameplay.MatchLogic.PointsLogic;
using Infrastructure.ServiceLogic;
using TMPro;
using UnityEngine;

namespace Gameplay.UILogic.InfoCanvasLogic.TeamPointsLogic
{
    public enum BlockTeamType
    {
        None,
        Player,
        Enemy
    }
    
    public class TeamPointsBlock : MonoBehaviour, ITeamPointsBlock
    {
        [SerializeField] private BlockTeamType _team;
        [SerializeField] private TextMeshProUGUI _valueText;

        private IPointsSystem _pointsSystem;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _pointsSystem = ServiceLocator.Get<IPointsSystem>();
            _pointsSystem.OnPointsAreReset += () => SetValue(0);

            switch (_team)
            {
                case BlockTeamType.Player:
                    _pointsSystem.OnPlayerTeamPointsChanged += SetValue;
                    break;
                case BlockTeamType.Enemy:
                    _pointsSystem.OnEnemyTeamPointsChanged += SetValue;
                    break;
            }   
            
            SetValue(0);
        }
        
        private void SetValue(int value)
        {
            _valueText.text = value.ToString();
        }
    }
}