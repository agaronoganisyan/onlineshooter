using Cysharp.Threading.Tasks;
using Infrastructure.ServiceLogic;
using UnityEngine.AddressableAssets;

namespace Infrastructure.SceneManagementLogic
{
    public interface ISceneSystem : IService
    {
        UniTask LoadScene(AssetReference sceneReference);
        UniTask UnloadScene();
        void Initialize();
    }
}