using Infrastructure.GameStateMachineLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace LobbyLogic
{
    public class LobbyPanel : MonoBehaviour, ILobbyPanel
    {
        private IGameStateMachine _gameStateMachine;
        
        public void Initialize()
        {
            _gameStateMachine = ServiceLocator.Get<IGameStateMachine>();
        }
        
        public void OnPlay()
        {
            _gameStateMachine.SwitchState(GameState.Match);
        }
        
        public void OnLeave()
        {
            _gameStateMachine.SwitchState(GameState.Lobby);
        }
    }
}