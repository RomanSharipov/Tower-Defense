using Assets.Scripts.Infrastructure.UI;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.Infrastructure
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
            _mainGameStatemachine.Enter<MenuState>();
            _uiFactory.CreateRootCanvas();
        }

        public  UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
