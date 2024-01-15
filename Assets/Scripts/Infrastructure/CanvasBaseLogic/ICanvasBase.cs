using Infrastructure.ServiceLogic;

namespace Infrastructure.CanvasBaseLogic
{
    public interface ICanvasBase : IService
    {
        void Initialize();
        void Show(bool withAnimation = false);
        void Hide(bool withAnimation = false);
    }
}