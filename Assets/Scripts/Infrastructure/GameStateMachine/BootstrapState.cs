using Cysharp.Threading.Tasks;
using UnityEngine;
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

        public  UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
