using Cysharp.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStatemachine _gameStatemachine;
        private readonly SceneLoader _sceneLoader;

        [Inject]
        public BootstrapState()
        {

        }

        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
