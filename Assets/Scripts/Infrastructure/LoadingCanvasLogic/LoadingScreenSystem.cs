using System;
using ConfigsLogic;
using Cysharp.Threading.Tasks;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;

namespace Infrastructure.LoadingCanvasLogic
{
    public class LoadingScreenSystem : CanvasSystemBase, ILoadingScreenSystem
    {
        private TimeSpan _showingDuration;
        
        public override void Initialize()
        {
            float showingDuration = ServiceLocator.Get<LoadingScreenSystemConfig>().ShowingDuration;
            _showingDuration = TimeSpan.FromSeconds(showingDuration);
        }

        public async UniTask Show()
        {
            base.Show(true);
            await UniTask.Delay(_showingDuration); 
        }

        public override void Hide(bool withAnimation = false)
        {
            base.Hide(true);
        }
    }
}