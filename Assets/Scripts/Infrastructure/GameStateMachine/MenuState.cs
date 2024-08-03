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
        
        private IWindowService _windowService;

        [Inject]
        public void Construct(ISceneLoader sceneLoader,IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _windowService = windowService;
        }

        public MenuState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        public async UniTask Enter()
        {
            _windowService.Open(WindowId.MainMenu).Forget();
        }

        public UniTask Exit()
        {
            _windowService.CloseAllWindows();
            return UniTask.CompletedTask;
        }
    }
}
