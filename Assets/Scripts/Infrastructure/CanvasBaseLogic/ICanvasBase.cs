using Infrastructure.ServiceLogic;

namespace Infrastructure.CanvasBaseLogic
{
    public interface ICanvasBase
    {
        void Initialize();
        void Show(bool withAnimation = false);
        void Hide(bool withAnimation = false);
    }
}