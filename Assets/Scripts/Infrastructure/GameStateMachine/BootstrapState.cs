using Cysharp.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private ISceneLoader _sceneLoader;
        private GameStatemachine _mainGameStatemachine;
        
        public BootstrapState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public UniTask Enter()
        {
            _mainGameStatemachine.Enter<MenuState>();
            return UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            await _sceneLoader.Load(SceneName.Menu);
        }
    }
}
