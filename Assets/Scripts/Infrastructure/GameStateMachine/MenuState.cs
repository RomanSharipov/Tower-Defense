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

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public MenuState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(SceneName.Menu);
            _uiFactory.CreateRootCanvas();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
