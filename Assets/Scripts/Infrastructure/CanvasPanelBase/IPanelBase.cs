namespace Infrastructure.CanvasPanelBase
{
    public interface IPanelBase
    {
        void Initialize();
        void Show(bool withAnimation = false);
        void Hide(bool withAnimation = false);
    }
}