using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagementLogic;
using Infrastructure.ServiceLogic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Infrastructure.SceneManagementLogic
{
    public enum SceneType
    {
        None,
        Lobby,
        Match
    }
    
    public class SceneSystem : ISceneSystem
    {
        private IAssetsProvider _assetsProvider;
        
        private SceneInstance _loadedSceneInstance;

        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async UniTask LoadScene(AssetReference sceneReference)
        {
            _loadedSceneInstance = await _assetsProvider.LoadScene(sceneReference);
        }

        public async UniTask UnloadScene()
        {
            await _assetsProvider.UnloadScene(_loadedSceneInstance);
        }
    }
}