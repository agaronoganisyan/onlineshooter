using System;
using Infrastructure.ServiceLogic;

namespace Infrastructure.CanvasBaseLogic
{
    public abstract class CanvasSystemBase : IService
    {
        public event Action<bool> OnShown;
        public event Action<bool> OnHidden;

        public virtual void Initialize()
        {
            
        }
        
        public virtual void Show(bool withAnimation = false)
        {
            OnShown?.Invoke(withAnimation);
        }

        public virtual void Hide(bool withAnimation = false)
        {
            OnHidden?.Invoke(withAnimation);
        }
    }
}