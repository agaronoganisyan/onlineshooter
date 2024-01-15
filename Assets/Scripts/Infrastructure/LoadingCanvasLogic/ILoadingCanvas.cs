using Cysharp.Threading.Tasks;
using Infrastructure.CanvasBaseLogic;

namespace Infrastructure.LoadingCanvasLogic
{
    public interface ILoadingCanvas : ICanvasBase
    {
        UniTask Show();
        void Hide();
    }
}