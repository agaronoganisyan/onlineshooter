using Cysharp.Threading.Tasks;

namespace Infrastructure.LoadingCanvasLogic
{
    public interface ILoadingCanvas
    {
        UniTask Show();
        void Hide();
    }
}