using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Infrastructure.AssetManagementLogic
{
    public interface IAssetsProvider
    {
        void CleanUp();
        UniTask<T> Load<T>(string address) where T : class;
        void Unload(string address);
        UniTask<SceneInstance> LoadScene(AssetReference sceneReference);
        UniTask UnloadScene(SceneInstance sceneReference);
        void Initialize();
    }
}