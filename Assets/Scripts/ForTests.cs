using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

public class ForTests : MonoBehaviour
{
    private ITeamsSystem _teamsSystem;

    [SerializeField] private Unit _unit;
        
    public void Initialize()
    {
        _teamsSystem = ServiceLocator.Get<ITeamsSystem>();
        _unit.Initialize();
    }

    [ContextMenu("INJECT")]
    public void INJECT()
    {
        _teamsSystem.AddUnitToTeam(_unit);
    }
}