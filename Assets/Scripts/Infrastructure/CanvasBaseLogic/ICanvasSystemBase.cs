using System;
using Infrastructure.ServiceLogic;

namespace Infrastructure.CanvasBaseLogic
{
    public interface ICanvasSystemBase : IService
    {
        event Action<bool> OnShown;
        event Action<bool> OnHidden;
        void Initialize();
        void Show(bool withAnimation = false);
        void Hide(bool withAnimation = false);
    }
}