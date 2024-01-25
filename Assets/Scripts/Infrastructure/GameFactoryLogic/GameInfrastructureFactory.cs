using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.UILogic.DebriefingCanvasLogic;
using Gameplay.UILogic.InfoCanvasLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic;
using Gameplay.UnitLogic.PlayerLogic;
using Infrastructure.AssetManagementLogic;
using Infrastructure.LoadingCanvasLogic;
using Infrastructure.ServiceLogic;
using InputLogic.InputCanvasLogic;
using LobbyLogic;
using UnityEngine;

namespace Infrastructure.GameFactoryLogic
{
    public class GameInfrastructureFactory : IGameInfrastructureFactory
    {
        private IAssetsProvider _assetsProvider;

        private string _playerAddress = "Player";
        private string _gameplayCameraAddress = "GameplayCamera";
        
        //Canvases
        private string _loadingCanvasAddress = "LoadingCanvas";
        private string _lobbyCanvasAddress = "LobbyCanvas";
        private string _inputCanvasAddress = "InputCanvas";
        private string _gameplayInfoCanvasAddress = "GameplayInfoCanvas";
        private string _sharedCanvasAddress = "SharedCanvas";
        private string _debriefingCanvasAddress = "DebriefingCanvas";

        // _sharedGameplayCanvas.StartUpdating();
        
        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async UniTask CreateAndRegisterInfrastructure()
        {
            await CreateLoadingCanvas();
            await CreatePlayer();
            await CreateCameraController();
            await CreateLobbyCanvas();
            await CreateInputCanvas();
            await CreateGameplayInfoCanvas();
            await CreateSharedCanvas();
            await CreateDebriefingCanvas();

        }

        private async UniTask CreatePlayer()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_playerAddress);
            Player obj = Object.Instantiate(prefab).GetComponent<Player>();
            ServiceLocator.Register<Player>(obj);
            obj.Initialize();
        }
        private async UniTask CreateCameraController()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_gameplayCameraAddress);
            IGameplayCamera obj = Object.Instantiate(prefab).GetComponent<IGameplayCamera>();
            ServiceLocator.Register<IGameplayCamera>(obj);
            obj.Initialize();
        }
        private async UniTask CreateLoadingCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_loadingCanvasAddress);
            ILoadingCanvas obj = Object.Instantiate(prefab).GetComponent<ILoadingCanvas>();
            //ServiceLocator.Register<ILoadingCanvas>(obj);
            obj.Initialize();
        }
        private async UniTask CreateLobbyCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_lobbyCanvasAddress);
            ILobbyCanvas obj = Object.Instantiate(prefab).GetComponent<ILobbyCanvas>();
            //ServiceLocator.Register<ILobbyCanvas>(obj);
            obj.Initialize();
        }
        private async UniTask CreateInputCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_inputCanvasAddress);
            IInputCanvas obj = Object.Instantiate(prefab).GetComponent<IInputCanvas>();
            //ServiceLocator.Register<IInputCanvas>(obj);
            obj.Initialize();
        }
        private async UniTask CreateGameplayInfoCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_gameplayInfoCanvasAddress);
            IGameplayInfoCanvas obj = Object.Instantiate(prefab).GetComponent<IGameplayInfoCanvas>();
            //ServiceLocator.Register<IGameplayInfoCanvas>(obj);
            obj.Initialize();
        }
        private async UniTask CreateSharedCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_sharedCanvasAddress);
            ISharedGameplayCanvas obj = Object.Instantiate(prefab).GetComponent<ISharedGameplayCanvas>();
            //ServiceLocator.Register<ISharedGameplayCanvas>(obj);
            obj.Initialize();
        }
        private async UniTask CreateDebriefingCanvas()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(_debriefingCanvasAddress);
            IDebriefingCanvas obj = Object.Instantiate(prefab).GetComponent<IDebriefingCanvas>();
            //ServiceLocator.Register<IDebriefingCanvas>(obj);
            obj.Initialize();
        }
    }
}