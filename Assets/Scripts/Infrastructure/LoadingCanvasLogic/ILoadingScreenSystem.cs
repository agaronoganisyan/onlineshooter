using Cysharp.Threading.Tasks;
using Infrastructure.CanvasBaseLogic;

namespace Infrastructure.LoadingCanvasLogic
{
    public interface ILoadingScreenSystem : ICanvasSystemBase
    {
        UniTask Show();
    }
}