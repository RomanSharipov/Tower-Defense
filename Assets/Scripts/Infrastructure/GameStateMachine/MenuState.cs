using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class MenuState : IState
    {
        private GameStatemachine _mainGameStatemachine;
        private ISceneLoader _sceneLoader;
        private IUIFactory _uiFactory;
        private IWindowService _windowService;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IUIFactory uiFactory, IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public MenuState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        public async UniTask Enter()
        {
            //await _sceneLoader.Load(SceneName.Menu);
            _windowService.Open(WindowId.MainMenu);
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
