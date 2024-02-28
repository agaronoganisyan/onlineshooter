using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using Gameplay.MatchLogic.TeamsLogic;
using Gameplay.UnitLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

public class ForTests : MonoBehaviour, IService
{
    private ITeamsSystem _teamsSystem;

    [SerializeField] private Unit _unit1;
    [SerializeField] private Unit _unit2;
    [SerializeField] private Unit _unit3;        
    public void Initialize()
    {
        _teamsSystem = ServiceLocator.Get<ITeamsSystem>();
        // _unit1.Initialize();
        // _unit2.Initialize();
        // _unit3.Initialize();
    }

    [ContextMenu("INJECT")]
    public void INJECT()
    {
        // _unit1.SetInfo("Player9430", TeamType.First);
        // _unit2.SetInfo("Player3294", TeamType.First);
        // _unit3.SetInfo("Player2743", TeamType.First);
        //
        // _teamsSystem.AddUnitToTeam(_unit1, TeamType.First);
        // _teamsSystem.AddUnitToTeam(_unit2, TeamType.First);
        // _teamsSystem.AddUnitToTeam(_unit3, TeamType.First);
    }
    
    [ContextMenu("RESPAWN_1")]
    public void RESPAWN_1()
    {
        SpawnPointInfo sp = new SpawnPointInfo();
        sp.Setup(new Vector3(-7.15f,0,1.67f), Quaternion.Euler(0,0,0));
        //_unit1.Respawn(sp);
    }
        
    [ContextMenu("RESPAWN_2")]
    public void RESPAWN_2()
    {
        SpawnPointInfo sp = new SpawnPointInfo();
        sp.Setup(new Vector3(-20.21f,0,0), Quaternion.Euler(0,-27.8f,0));
        //_unit2.Respawn(sp);
    }
        
    [ContextMenu("RESPAWN_3")]
    public void RESPAWN_3()
    {
        SpawnPointInfo sp = new SpawnPointInfo();
        sp.Setup(new Vector3(-46.47f,0,19.87f), Quaternion.Euler(0,108.7f,0));
        //_unit3.Respawn(sp);
    }
}