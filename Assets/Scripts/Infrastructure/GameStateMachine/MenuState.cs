using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class MenuState : IState
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IWindowService _windowService;

        [Inject]
        public MenuState(IAssetProvider assetProvider, IWindowService windowService)
        {
            _assetProvider = assetProvider;
            _windowService = windowService;
        }

        public async UniTask Enter()
        {
            _windowService.Open(WindowId.MainMenu).Forget();
        }

        public UniTask Exit()
        {
            _windowService.CloseAllWindows();
            _assetProvider.Cleanup();
            return UniTask.CompletedTask;
        }
    }
}
