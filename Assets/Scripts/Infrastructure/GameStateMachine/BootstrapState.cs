using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private ISceneLoader _sceneLoader;
        private IUIFactory _uiFactory;
        private IAssetProvider _assetProvider;
        private GameStatemachine _mainGameStatemachine;
        
        public BootstrapState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IUIFactory uiFactory, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            await _assetProvider.Initialize();
            await _uiFactory.CreateRootCanvas();
            _mainGameStatemachine.Enter<MenuState>();
        }

        public  UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
