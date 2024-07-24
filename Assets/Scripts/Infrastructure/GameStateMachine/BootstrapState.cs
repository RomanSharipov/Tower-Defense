using Assets.Scripts.Infrastructure.UI;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private ISceneLoader _sceneLoader;
        private IUIFactory _uiFactory;
        private GameStatemachine _mainGameStatemachine;
        
        public BootstrapState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public UniTask Enter()
        {
            _mainGameStatemachine.Enter<MenuState>();
            _uiFactory.CreateRootCanvas();
            return UniTask.CompletedTask;
        }

        public  UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
