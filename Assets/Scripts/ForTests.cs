using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
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
        _unit.SetInfo("96969506870",TeamType.Second);
        _teamsSystem.AddUnitToTeam(_unit);
    }
        
    [ContextMenu("RESPAWN")]
    public void RESPAWN()
    {
        SpawnPointInfo sp = new SpawnPointInfo();
        sp.Setup(new Vector3(0,0,0), Quaternion.Euler(0,0,0));
        _unit.Respawn(sp);
    }
}