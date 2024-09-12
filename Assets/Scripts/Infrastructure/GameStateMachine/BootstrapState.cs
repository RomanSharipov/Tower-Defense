using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IAppStateService _iAppStateService;
        
        [Inject]
        public BootstrapState(ISceneLoader sceneLoader, IUIFactory uiFactory, IAssetProvider assetProvider, IAppStateService iAppStateService)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _iAppStateService = iAppStateService;
        }

        public async UniTask Enter()
        {
            await _assetProvider.Initialize();
            await _uiFactory.CreateRootCanvas();
            _iAppStateService.GoToState(State.MenuState);
        }

        public  UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
